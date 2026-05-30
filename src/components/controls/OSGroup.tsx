import type ProductionUnit from "../../models/ProductionUnit";
import locale from "../../data/locale";
import HeaderGroup from "../containers/HeaderGroup";
import Label from "./Label";


export default function OSGroup(props: { unit: ProductionUnit, size: number }) {
  const { unit: { powerShardCount, somersloopCount }, size } = props;
  return (
    <div className="stack-panel-h middle-gap"
      hidden={!(powerShardCount || somersloopCount)}>
      {Boolean(powerShardCount) &&
        <HeaderGroup headerTitle={locale.current["PowerShards"]}>
          <Label
            content={["SyntheticPowerShard", powerShardCount]}
            colors={["goldenrod", "sienna"]}
            size={size} />
        </HeaderGroup>}
      {Boolean(somersloopCount) &&
        <HeaderGroup headerTitle={locale.current["Somersloops"]}>
          <Label
            content={["Somersloop", somersloopCount]}
            colors={["darkviolet", "darkmagenta"]}
            size={size} />
        </HeaderGroup>}
    </div>
  );
}