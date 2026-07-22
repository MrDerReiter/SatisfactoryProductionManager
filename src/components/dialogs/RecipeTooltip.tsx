import type Recipe from "../../models/Recipe";
import locale from "../../data/locale";
import Label from "../controls/Label";


const commonImageSize = 35;

function IOGroup(props: { header: string, group: [string, number][] }) {
  const { header, group } = props;
  return (
    <>
      <p>{header}</p>
      <div className="stack-panel-h small-gap bordered-wrapper">
        {group.map(entry =>
          <Label
            content={entry}
            size={commonImageSize}
            colors={["inherit", "inherit"]}
            tooltip={locale.current[entry[0]]}
            key={entry.join(" ")} />)}
      </div>
    </>
  );
}


export default function RecipeTooltip({ recipe }: { recipe: Recipe }) {
  return (
    <div className="recipe-tooltip stack-panel-v">
      <p className="recipe-header">{locale.current[recipe.name]}</p>
      <IOGroup header={locale.current["Products"] + ":"} group={recipe.outputs} />
      <IOGroup header={locale.current["Intake"] + ":"} group={recipe.inputs} />
    </div>
  );
}