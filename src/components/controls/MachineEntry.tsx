import type ProductionUnit from "../../models/ProductionUnit";
import { formatter, handleError } from "../../service/utils";
import locale from "../../data/locale";
import HeaderGroup from "../containers/HeaderGroup";
import ImageEntry from "./ImageEntry";


export default function MachineEntry(props: { unit: ProductionUnit, size: number }) {
  function onMachineCountChanged(count: number): string | null {
    try { unit.update(["machineCount", count]); return null; }
    catch (error) {
      handleError(error as Error);
      return formatter.format(machineCount);
    }
  }

  const { unit, size, unit: { machine, machineCount } } = props;
  return (
    <HeaderGroup headerTitle={locale.current["Machines"]}>
      <ImageEntry
        content={[`images/Machines/${machine}.png`, machineCount]}
        colors={["khaki", "darkkhaki"]}
        callback={onMachineCountChanged}
        tooltip={locale.current[machine]}
        size={size} />
    </HeaderGroup>
  );
}