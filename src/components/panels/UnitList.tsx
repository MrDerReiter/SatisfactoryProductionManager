import type { PropsWithChildren } from "react";
import type ProductionUnit from "../../models/ProductionUnit";
import locale from "../../data/locale";
import UnitToolbar from "../controls/UnitToolbar";
import LabelGroup from "../controls/LabelGroup";
import MachineEntry from "../controls/MachineEntry";
import OSGroup from "../controls/OSGroup";


const commonImageSize = 65;

export default function UnitList(props: PropsWithChildren) {
  return <ul className="stack-panel-v unit-list">{props.children}</ul>;
}

UnitList.Item = (props: { unit: ProductionUnit }) => {
  const { unit } = props;
  const outputs = Object.entries(unit.IO.outputs);
  const inputs = Object.entries(unit.IO.inputs);

  return (
    <li className="stack-panel-h width-scrollable middle-gap">
      <div className="stack-panel-h">
        <UnitToolbar unit={unit} />
        <MachineEntry unit={unit} size={commonImageSize} />
      </div>
      <LabelGroup
        title={locale.current["Output"]}
        size={commonImageSize}
        colors={["lightblue", "cadetblue"]}
        values={outputs} />
      {inputs.length > 0 ?
        <LabelGroup
          title={locale.current["Input"]}
          size={commonImageSize}
          colors={["lightgreen", "forestgreen"]}
          values={inputs} /> : null}
      <OSGroup unit={unit} size={commonImageSize} />
    </li>
  );
}