import locale from "../data/locale";
import { ImmutableList } from "../service/FP";
import { mergeIO, balanceIO, handleError } from "../service/utils";
import ProductionUnit, { type UnitCtorArgs, type UnitProp } from "./ProductionUnit";
import Recipe from "./Recipe";


interface updateEventArgs {
  sender: ProductionUnit,
  newState: ProductionUnit,
  propertyChanged: UnitProp,
  parentBlock: ProductionBlock
}
interface cascadeActionArgs {
  child: ProductionUnit;
  parent: ProductionUnit[];
}
interface cascadeUpdateArgs extends cascadeActionArgs {
  newChild: ProductionUnit;
  ratio: number;
}

function updateHandler(event: updateEventArgs) {
  const { sender, newState, propertyChanged, parentBlock } = event;

  switch (propertyChanged) {
    case "machineCount":
    case "overclock": parentBlock.replace(sender, newState); break;

    case "request":
      parentBlock.onChange?.(new ProductionBlock(...cascadeUpdate({
        child: sender,
        newChild: newState,
        parent: Array.from(parentBlock),
        ratio: newState.product[1] / sender.product[1]
      }))); break;

    case "isSomersloopUsed":
      parentBlock.onChange?.(new ProductionBlock(...cascadeUpdate({
        child: sender,
        newChild: newState,
        parent: Array.from(parentBlock),
        ratio: newState.isSomersloopUsed ? 0.5 : 2
      }))); break;
  }
}

function cascadeUpdate(args: cascadeUpdateArgs): ProductionUnit[] {
  const { child, newChild, parent, ratio } = args;

  for (const resource in child.inputs) {
    parent.filter(unit => unit.product[0] == resource)
      .forEach(provider => {
        const newRequesValue = provider.product[1] > child.inputs[resource] ?
          provider.product[1] - child.inputs[resource] + (child.inputs[resource] * ratio) :
          provider.product[1] * ratio;

        cascadeUpdate({
          child: provider,
          newChild: provider.modify(["request", newRequesValue]),
          parent,
          ratio
        });
      });
  }

  parent[parent.indexOf(child)] = newChild;
  return parent;
}

function cascadeRemove(args: cascadeActionArgs): ProductionUnit[] {
  const { child, parent } = args;
  for (const resource in child.inputs) {
    parent.filter(unit => unit.product[0] == resource)
      .forEach(provider => {
        if (provider.product[1] > child.inputs[resource]) {
          queueMicrotask(() =>
            provider.update(["request", provider.product[1] - child.inputs[resource]]));
        } else cascadeRemove({ child: provider, parent });
      });
  }

  parent.splice(parent.indexOf(child), 1);
  return parent;
}

function getUnitExtensions(parentBlock: ProductionBlock) {
  return {
    onPropertyChanged(this: ProductionUnit, propertyChanged: UnitProp, newState: ProductionUnit) {
      updateHandler({ sender: this, newState, propertyChanged, parentBlock });
    },

    onRemove(this: ProductionUnit) {
      if (parentBlock.indexOf(this) == 0) {
        handleError(new Error(locale.current["CannotRemoveMainUnit"]));
        return;
      }

      parentBlock.onChange?.(new ProductionBlock(...cascadeRemove({
        parent: Array.from(parentBlock),
        child: this
      })));
    },

    onDeploy(this: ProductionUnit) {
      if (parentBlock.indexOf(this) == 0) {
        handleError(new Error(locale.current["CannotRemoveMainUnit"]));
        return;
      }

      import("./Factory.js")
        .then(module => module.addProductionBlock(new ProductionBlock(this)));

      parentBlock.onChange?.(new ProductionBlock(...cascadeRemove({
        parent: Array.from(parentBlock),
        child: this
      })));
    }
  }
}


export default class ProductionBlock extends ImmutableList<ProductionUnit> {
  declare onRemove?: () => void;
  declare onRequestRecipe?: (request: [string, number]) => Promise<Recipe>;

  constructor(...args: UnitCtorArgs)
  constructor(...args: ProductionUnit[])
  constructor(...args: any[]) {
    if (args[0] instanceof Recipe) {
      super(new ProductionUnit(...args as UnitCtorArgs));
    } else super(...args);

    const extensions = getUnitExtensions(this);
    this.forEach(unit => Object.assign(unit, extensions));
  }


  get IO() { return balanceIO(this.map(unit => unit.IO).reduce(mergeIO)); }
  get request() { return this[0].product[1]; }
  set request(value) {
    if (value < 0) throw new Error(locale.current["SubZeroRequestValue"]);
    if (value == this.request) return;
    this[0].update(["request", value]);
  }

  get powerShardCount(): number {
    return [...this]
      .map(unit => unit.powerShardCount)
      .reduce((prev, next) => prev + next);
  }

  get somersloopCount(): number {
    return [...this]
      .map(unit => unit.somersloopCount)
      .reduce((prev, next) => prev + next);
  }


  override replace(prevChild: ProductionUnit, newChild: ProductionUnit): this {
    const newState = new ProductionBlock(...Array.from(this).map(unit => {
      if (unit == prevChild) return newChild;
      else return unit;
    }));

    this.onChange?.(newState);
    return newState as this;
  }

  async deployRequest(request: [string, number]) {
    try {
      const recipe = await this.onRequestRecipe?.(request);
      if (recipe) this.append(new ProductionUnit(recipe, request[1]));
    } catch (error) { handleError(error as Error); }
  }

  remove() { this.onRemove?.(); }
}