import { useEffect, useRef, useState } from "react";
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
    return dialog.current?.open(resolve => dialogResolver = resolve);
  }

  const [dict, setDict] = useState(locale.current);
  const [request, setRequest] = useState<[string, number]>();
  const [category, setCategory] = useState<Recipe[]>();
  const tooltip = useRef<TooltipDispatcher<RecipeProps>>(null);
  const dialog = useRef<DialogCaller<Recipe>>(null);

  useEffect(() => {
    locale.onChange(setDict);
    externals.getUserSelectedRecipe = activateSelector
  }, []);

  return (
    <Modal<Recipe> className="main-panel" callerRef={dialog}>
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
          className="height-scrollable cat-overview"
          tooltipContent={RecipeTooltip}
          dispatcherRef={tooltip}>
          {(category?.map(recipe =>
            <RecipeButton
              recipe={recipe}
              callback={userSelectsRecipe}
              onHover={(event, recipe) => tooltip.current?.show(event, { recipe })}
              onLeave={() => tooltip.current?.hide()}
              size={buttonsSize}
              key={recipe.name} />))}
        </TooltipProvider>
      </div>
    </Modal>
  );
}