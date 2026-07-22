import type ProductionUnit from "../../models/ProductionUnit";
import locale from "../../data/locale";
import UnitToolbar from "../controls/UnitToolbar";
import LabelGroup from "../controls/LabelGroup";
import MachineEntry from "../controls/MachineEntry";
import OSGroup from "../controls/OSGroup";
import Show from "../containers/Show";


type UnitListItem = ReturnType<typeof UnitList.Item>;
const commonImageSize = 65;

export default function UnitList(props: { children: UnitListItem[] }) {
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
      <Show when={inputs.length > 0}>
        <LabelGroup
          title={locale.current["Input"]}
          size={commonImageSize}
          colors={["lightgreen", "forestgreen"]}
          values={inputs} />
      </Show>
      <OSGroup unit={unit} size={commonImageSize} />
    </li>
  );
}