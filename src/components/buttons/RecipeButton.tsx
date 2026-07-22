import type { MouseEvent } from "react";
import type Recipe from "../../models/Recipe";
import ImageButton from "./ImageButton";

interface RecipeButtonProps {
  recipe: Recipe;
  size: number;
  callback: (recipe: Recipe) => void;
  onHover: (event: MouseEvent, recipe: Recipe) => void;
  onLeave: VoidFunction;
}

export default function RecipeButton(props: RecipeButtonProps) {
  const { size, recipe, onHover, onLeave, callback } = props;
  return (
    <ImageButton
      image={recipe.category == "PowerGenerating" ?
        `images/Resources/${recipe.inputs[0][0]}.png` :
        `images/Resources/${recipe.product[0]}.png`}
      className="production-button"
      callback={() => callback(recipe)}
      onHover={(event) => onHover(event, recipe)}
      onLeave={onLeave}
      size={size} />
  );
}