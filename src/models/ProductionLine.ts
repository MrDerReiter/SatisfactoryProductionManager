import type { UnitCtorArgs } from "./ProductionUnit.js";
import { ImmutableList } from "../service/FP.js";
import { mergeIO, balanceIO, type IO } from "../service/utils.js";
import ProductionBlock from "./ProductionBlock.js";
import Recipe from "./Recipe.js";


export default class ProductionLine extends ImmutableList<ProductionBlock> {
  declare onMove?: (direction: "left" | "right") => void;
  declare onRemove?: () => void;


  constructor(...args: UnitCtorArgs)
  constructor(...args: ProductionBlock[])
  constructor(...args: any[]) {
    if (args[0] instanceof Recipe) super(new ProductionBlock(...args));
    else super(...args);
  }

  get IO(): IO { return balanceIO(this.map(block => block.IO).reduce(mergeIO)); }
  move(direction: "left" | "right") { this.onMove?.(direction); }
  remove() { this.onRemove?.(); }
}