import type ProductionUnit from "../../models/ProductionUnit";
import locale from "../../data/locale";
import HeaderGroup from "../containers/HeaderGroup";
import Label from "./Label";
import Show from "../containers/Show";


export default function OSGroup(props: { unit: ProductionUnit, size: number }) {
  const { unit: { powerShardCount, somersloopCount }, size } = props;
  return (
    <div className="stack-panel-h middle-gap"
      hidden={!(powerShardCount || somersloopCount)}>
      <Show when={powerShardCount > 0}>
        <HeaderGroup headerTitle={locale.current["PowerShards"]}>
          <Label
            content={["SyntheticPowerShard", powerShardCount]}
            colors={["goldenrod", "sienna"]}
            size={size} />
        </HeaderGroup>
      </Show>
      <Show when={somersloopCount > 0}>
        <HeaderGroup headerTitle={locale.current["Somersloops"]}>
          <Label
            content={["Somersloop", somersloopCount]}
            colors={["darkviolet", "darkmagenta"]}
            size={size} />
        </HeaderGroup>
      </Show>
    </div>
  );
}