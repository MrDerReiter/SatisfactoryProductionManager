import type ProductionUnit from "../../models/ProductionUnit";
import { handleError } from "../../service/utils";
import locale from "../../data/locale";
import VerticalToolbar, { type ButtonSchema } from "../containers/VerticalToolbar";
import ValueEntry from "./ValueEntry";


function getSchemas(unit: ProductionUnit): ButtonSchema[] {
  return [
    {
      image: "assets/AddItem.png",
      tooltip: locale.current["DeployUnitToBlock"],
      callback: () => unit.deploy()
    },
    {
      image: "assets/DeleteItem.png",
      tooltip: locale.current["RemoveUnit"],
      callback: () => unit.remove()
    },
    {
      image: "assets/Resources/Somersloop.png",
      tooltip: locale.current["UseSomersloop"],
      specialClass: unit.isSomersloopUsed ? "somersloop-used" : undefined,
      callback: () => unit.update(["isSomersloopUsed", !unit.isSomersloopUsed])
    }
  ]
}

const formatter = new Intl.NumberFormat("en-US", {
  minimumFractionDigits: 0,
  maximumFractionDigits: 1,
  useGrouping: false
});


export default function UnitToolbar(props: { unit: ProductionUnit }) {
  function onOverclockChanged(value: number) {
    try { unit.update(["overclock", value]); return null; }
    catch (error) {
      handleError(error as Error);
      return formatter.format(unit.overclock) + "%";
    }
  }

  const { unit } = props;
  return (
    <VerticalToolbar
      buttonsClass="command-button"
      buttons={getSchemas(unit)}
      size={20}>
      <ValueEntry
        initValue={formatter.format(unit.overclock) + "%"}
        className="overclock-text-box"
        tooltip={locale.current["Overclock"]}
        handler={onOverclockChanged} />
    </VerticalToolbar>
  );
}