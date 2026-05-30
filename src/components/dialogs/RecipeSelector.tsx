import { useEffect, useState } from "react";
import locale from "../../data/locale";
import Modal, { type DialogCaller } from "./Modal";
import TooltipProvider, { type TooltipDispatcher } from "./TooltipProvider";
import RecipeButton from "../buttons/RecipeButton";
import RecipeTooltip from "./RecipeTooltip";
import Recipe from "../../models/Recipe";


interface RecipeProps { recipe: Recipe };
interface ExternalFunctions {
  getUserSelectedRecipe?: (request?: [string, number]) => Promise<Recipe | void>
}
export const externals: ExternalFunctions = {};

let dialogResolver: (recipe: Recipe) => void;
const userSelectsRecipe = (recipe: Recipe) => dialogResolver(recipe);
const dialog: DialogCaller<Recipe> = {};
const tooltip: TooltipDispatcher<RecipeProps> = {};
const buttonsSize = 35;
const catNames = [
  "Ingots",
  "Minerals",
  "StandartParts",
  "IndustrialParts",
  "Electronics",
  "Communications",
  "QuantumTech",
  "Converting",
  "Supplies",
  "Liquids",
  "Packages",
  "Burnable",
  "Nuclear",
  "PowerGenerating"
];

const categories: Record<string, Recipe[]> = Object.fromEntries
  (catNames.map(name => [name, Recipe.getAllWithCategory(name)]));


export default function RecipeSelector() {
  async function activateSelector(request?: [string, number]): Promise<Recipe | void> {
    if (request) {
      const recipes = Recipe.getAllWithProduct(request[0]);
      if (recipes.length == 0)
        throw new Error(`${locale.current["RecipeNotFound"]} "${locale.current[request[0]]}".`);
      if (recipes.length == 1) return recipes[0];

      setCategory(recipes);
    } else setCategory(categories["Ingots"]);

    setRequest(request);
    return dialog.open!(resolve => dialogResolver = resolve);
  }

  const [dict, setDict] = useState(locale.current);
  const [request, setRequest] = useState<[string, number]>();
  const [category, setCategory] = useState<Recipe[]>();

  useEffect(() => {
    locale.onChange(setDict);
    externals.getUserSelectedRecipe = activateSelector
  }, []);

  return (
    <Modal<Recipe> className="main-panel" caller={dialog}>
      <div className="stack-panel-h recipe-selector">
        <div className="stack-panel-v">
          {request ? <p className="selector-button"> {dict["ProperRecipes"]}</p> :
            catNames.map(name =>
              <p onClick={() => setCategory(categories[name])}
                className="selector-button"
                key={name}>
                {dict[name]}
              </p>)}
        </div>
        <TooltipProvider
          containerClass="height-scrollable cat-overview"
          tooltipClass="tooltip"
          tooltipContent={RecipeTooltip}
          dispatcher={tooltip}>
          {(category?.map(recipe =>
            <RecipeButton
              recipe={recipe}
              callback={userSelectsRecipe}
              onHover={(event, recipe) => tooltip.show(event, { recipe })}
              onLeave={() => tooltip.hide()}
              size={buttonsSize}
              key={recipe.name} />))}
        </TooltipProvider>
      </div>
    </Modal>
  );
}