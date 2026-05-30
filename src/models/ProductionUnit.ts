import locale from "../data/locale";
import type Recipe from "./Recipe";
import type { IO } from "../service/utils";


export type UnitCtorArgs = ConstructorParameters<typeof ProductionUnit>;
export type UnitProp = "request" | "overclock" | "machineCount" | "isSomersloopUsed";
type PropEntry = [Exclude<UnitProp, "isSomersloopUsed">, number] | ["isSomersloopUsed", boolean];

function multiple(arr: [string, number], multiplier: number): [string, number]
function multiple(arr: [string, number][], multiplier: number): [string, number][]
function multiple(arr: any[], multiplier: number) {
  if (arr[0] instanceof Array)
    return arr.map(entry => [entry[0], entry[1] * multiplier]);
  else return [arr[0], arr[1] * multiplier];
}

function calcOverclockFromMachineCount(unit: ProductionUnit, newMachineCount: number): number {
  const overclock = unit.overclock / (newMachineCount / unit.machineCount);
  if (overclock > 250) throw new Error(locale.current["NotEnoughMachines"]);

  return overclock;
}

const defaultOptions = { overclock: 100, isSomersloopUsed: false };
const somersloopsPerMachine: Record<string, number> = {
  Constructor: 1,
  Smelter: 1,
  Assembler: 2,
  Refinery: 2,
  Converter: 2,
  Foundry: 2,
  Manufacturer: 4,
  Blender: 4,
  Collider: 4,
  QuantumEncoder: 4
} as const;


export default class ProductionUnit {
  declare onPropertyChanged?: (property: UnitProp, newState: ProductionUnit) => void;
  declare onRemove?: () => void;
  declare onDeploy?: () => void;
  private readonly _recipe: Recipe;

  product: [string, number];
  byproduct?: [string, number];
  inputs: Record<string, number>;
  machine: string;
  machineCount: number;
  overclock: number;
  isOverclocked: boolean;
  isSomersloopUsed: boolean;
  powerShardCount: number;
  somersloopCount: number;


  constructor(recipe: Recipe, request = 0, options = defaultOptions) {
    this._recipe = recipe;

    const [resource, baseRequest] = recipe.product;
    const baseMachineCount = request / baseRequest;
    const overclockModifier = options.overclock / 100;
    const somersloopModifier = options.isSomersloopUsed ? 2 : 1;
    const machineCount = baseMachineCount / overclockModifier / somersloopModifier;
    const isOverclocked = options.overclock > 100 && machineCount >= 1;

    const powerShardCount = isOverclocked ?
      Math.floor(machineCount) * Math.ceil((options.overclock - 100) / 50) : 0;
    const somersloopCount = options.isSomersloopUsed ?
      Math.ceil(machineCount) * somersloopsPerMachine[recipe.machine] : 0;

    this.product = [resource, request];
    if (recipe.byproduct) this.byproduct = multiple(recipe.byproduct, baseMachineCount);

    this.inputs = recipe.inputs.length == 0 ? {} :
      Object.fromEntries(multiple(recipe.inputs, baseMachineCount / somersloopModifier));

    this.machine = recipe.machine;
    this.machineCount = machineCount;
    this.isOverclocked = isOverclocked;
    this.overclock = options.overclock;
    this.isSomersloopUsed = options.isSomersloopUsed;
    this.powerShardCount = powerShardCount;
    this.somersloopCount = somersloopCount;
  }

  static from(template: Partial<ProductionUnit>): ProductionUnit {
    return Object.setPrototypeOf(template, ProductionUnit.prototype);
  }

  get IO(): IO {
    const outputEntries = this.byproduct ?
      [this.product, this.byproduct] : [this.product];

    return {
      inputs: Object.assign({}, this.inputs),
      outputs: Object.fromEntries(outputEntries)
    };
  }

  modify(change: PropEntry): ProductionUnit {
    const args: UnitCtorArgs = [this._recipe];
    const [prop, value] = change;

    switch (prop) {
      case "request": args.push(value, {
        overclock: this.overclock,
        isSomersloopUsed: this.isSomersloopUsed
      }); break;

      case "overclock":
        if (value < 0 || value > 250) throw new Error(locale.current["InvalidOverclockValue"]);
        args.push(this.product[1], {
          overclock: value,
          isSomersloopUsed: this.isSomersloopUsed
        }); break;

      case "machineCount": args.push(this.product[1], {
        overclock: calcOverclockFromMachineCount(this, value),
        isSomersloopUsed: this.isSomersloopUsed
      }); break;

      case "isSomersloopUsed": args.push(this.product[1], {
        overclock: this.overclock,
        isSomersloopUsed: value
      }); break;
    }

    return new ProductionUnit(...args);
  }

  update(change: PropEntry) {
    const newState = this.modify(change);
    this.onPropertyChanged?.(change[0], newState);
  }

  remove() { this.onRemove?.() }

  deploy() { this.onDeploy?.() }
}