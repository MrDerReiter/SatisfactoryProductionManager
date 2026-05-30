import locale from "../data/locale";
import templates from "../data/recipes";


export default class Recipe {
  declare name: string;
  declare category: string;
  declare machine: string;
  declare outputs: [string, number][];
  declare inputs: [string, number][];


  get product() { return this.outputs[0]; }
  get byproduct() { return this.outputs[1] ?? null; }

  static get(name: string): Recipe {
    const template = (templates as Recipe[]).find(item => item.name == name);
    if (template) return template;
    else throw new Error(`${locale.current["RecipeDoesNotExist"]} "${locale.current[name]}".`);
  }

  static getAllWithProduct(resource: string): Recipe[] {
    return (templates as Recipe[]).filter(recipe => recipe.product[0] == resource);
  }

  static getAllWithCategory(category: string): Recipe[] {
    return (templates as Recipe[]).filter(recipe => recipe.category == category);
  }
}


for (const template of templates)
  Object.setPrototypeOf(template, Recipe.prototype);