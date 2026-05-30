import type Recipe from "./Recipe.js";
import { externals as recipeSelector } from "../components/dialogs/RecipeSelector";
import { handleError } from "../service/utils.js";
import { ImmutableList } from "../service/FP.js";
import ProductionLine from "./ProductionLine.js";
import ProductionBlock from "./ProductionBlock.js";
import ProductionUnit, { type UnitCtorArgs } from "./ProductionUnit.js";


type FactoryItem = ImmutableList<ProductionLine> | ProductionLine | ProductionBlock;
type FactoryItemName = "productionLines" | "activeLine" | "activeBlock";
type ModelName = "line" | "block" | "unit";
type UICallback<T extends FactoryItem> = (item: T) => void;
type PropValue<Prop extends FactoryItemName> =
  Prop extends "productionLines" ? ImmutableList<ProductionLine> :
  Prop extends "activeLine" ? ProductionLine :
  Prop extends "activeBlock" ? ProductionBlock : never;

interface ChangedProps {
  productionLines?: ImmutableList<ProductionLine>;
  activeLine?: ProductionLine;
  activeBlock?: ProductionBlock;
}


class FactoryStateHandler {
  get productionLines() { return productionLines; }
  get activeLine() { return activeLine; }
  set activeLine(line) {
    if (line == activeLine) return;
    callListeners({
      activeBlock: activeBlock = line[0],
      activeLine: activeLine = line
    });
  }

  get activeBlock() { return activeBlock; }
  set activeBlock(block) {
    if (block == activeBlock) return;
    callListeners({ activeBlock: activeBlock = block });
  }
}

const listeners = new Map<string, UICallback<FactoryItem>[]>();
let productionLines: ImmutableList<ProductionLine>;
let activeLine: ProductionLine;
let activeBlock: ProductionBlock;

const listExtensions = {
  onChange(newState: ImmutableList<ProductionLine>) {
    productionLines = newState;
    callListeners({ productionLines });
  }
};

const lineExtensions = {
  onChange(this: ProductionLine, newState: ProductionLine) {
    activeLine = newState;
    callListeners({ activeLine });
    productionLines.replace(this, activeLine);
  },

  onRemove(this: ProductionLine) {
    const index = productionLines.indexOf(this);
    const nextActiveLine = productionLines[index - 1] ?? productionLines[index + 1];
    const nextActiveBlock = nextActiveLine?.[0];

    callListeners({
      activeBlock: activeBlock = nextActiveBlock,
      activeLine: activeLine = nextActiveLine
    });

    productionLines.cut(this);
  },

  onMove(this: ProductionLine, direction: "left" | "right") {
    const index = productionLines.indexOf(this);
    switch (direction) {
      case "left":
        if (index == 0) return;
        else productionLines.swap(productionLines[index - 1], this); break;

      case "right":
        if (index == productionLines.length - 1) return;
        else productionLines.swap(this, productionLines[index + 1]); break;
    }
  }
};

const blockExtensions = {
  onChange(this: ProductionBlock, newState: ProductionBlock) {
    activeBlock = newState;
    callListeners({ activeBlock });
    activeLine.replace(this, activeBlock);
  },

  onRemove(this: ProductionBlock) {
    const index = activeLine.indexOf(this);
    if (index == 0) { activeLine.remove(); return; }

    callListeners({ activeBlock: activeBlock = activeLine[index - 1] });
    activeLine.cut(this);
  },

  onRequestRecipe(request: [string, number]) {
    return recipeSelector.getUserSelectedRecipe!(request);
  }
};


export const state = new FactoryStateHandler();

export function onChange<T extends FactoryItemName>(prop: T, callback: UICallback<PropValue<T>>) {
  if (!listeners.has(prop)) listeners.set(prop, []);
  listeners.get(prop)!.push(callback as UICallback<FactoryItem>);
}

export async function addProductionLine() {
  const line = await createModel("line");
  if (!line) return;

  callListeners({
    activeBlock: activeBlock = line[0],
    activeLine: activeLine = line
  });

  productionLines.append(line);
}

export async function addProductionBlock(request?: [string, number]): Promise<void>
export async function addProductionBlock(block: ProductionBlock): Promise<void>
export async function addProductionBlock(arg?: [string, number] | ProductionBlock) {
  const block = arg instanceof ProductionBlock ? arg : await createModel("block", arg);
  if (!block) return;

  callListeners({ activeBlock: activeBlock = block });
  activeLine.append(block);
}


async function createModel<T extends ModelName>(modelName: T, request?: [string, number]) {
  type Model<T extends ModelName> =
    T extends "line" ? ProductionLine :
    T extends "block" ? ProductionBlock :
    T extends "unit" ? ProductionUnit : never;

  let recipe: Recipe | void;
  try {
    recipe = await recipeSelector.getUserSelectedRecipe!(request);
    if (!recipe) return null;
  } catch (error) {
    handleError(error as Error);
    return null;
  }

  const args: UnitCtorArgs = [recipe, request ? request[1] : recipe.product[1]];
  switch (modelName) {
    case "line": return new ProductionLine(...args) as Model<T>;
    case "block": return new ProductionBlock(...args) as Model<T>;
    case "unit": return new ProductionUnit(...args) as Model<T>;
  }
}

function callListeners(changes: ChangedProps) {
  for (const prop in changes)
    listeners.get(prop)?.forEach(callback =>
      callback(changes[prop as keyof ChangedProps]!));
}

function loadFactory() {
  const savedData = localStorage.getItem("productionLines");
  if (!savedData) { productionLines = new ImmutableList(); return; }

  const linesTemplates = JSON.parse(savedData) as Partial<ProductionUnit>[][][];
  if (linesTemplates.length == 0) { productionLines = new ImmutableList(); return; }

  const lines = linesTemplates.map(lineObject =>
    new ProductionLine(...lineObject.map(blockObject =>
      new ProductionBlock(...blockObject.map(unitTemplate =>
        ProductionUnit.from(unitTemplate))))));

  productionLines = new ImmutableList(...lines);
  activeLine = productionLines[0];
  activeBlock = activeLine[0];
}

function saveFactory() {
  const factoryData = JSON.stringify(productionLines);
  localStorage.setItem("productionLines", factoryData);
}


Object.assign(ImmutableList.prototype, listExtensions);
Object.assign(ProductionLine.prototype, lineExtensions);
Object.assign(ProductionBlock.prototype, blockExtensions);
document.onvisibilitychange = () => saveFactory();
loadFactory();