import { useEffect, useState } from "react";
import { formatter, handleError } from "../../service/utils";
import { state, onChange } from "../../models/Factory";
import { getUniqueID } from "../../service/IDGen";
import locale from "../../data/locale";
import ProductEntry from "../controls/ProductEntry";
import OutputGroup from "../controls/OutputGroup";
import InputGroup from "../controls/InputGroup";
import UnitList from "./UnitList";


const IOPanelSize = 45;
const deployRequest = (request: [string, number]) => state.activeBlock.deployRequest(request);
const updateRequest = (request: number) => {
  try {
    state.activeBlock.request = request;
    return null;
  } catch (error) {
    handleError(error as Error);
    return formatter.format(state.activeBlock.request);
  }
}


export default function BlockWorkspace() {
  const [dict, setDict] = useState(locale.current);
  const [activeBlock, setActiveBlock] = useState(state.activeBlock);

  useEffect(() => {
    locale.onChange(setDict);
    onChange("activeBlock", setActiveBlock);
  }, []);

  return (
    <div className="stack-panel-v block-workspace">
      <div className="stack-panel-h width-scrollable">
        <OutputGroup content={activeBlock.IO.outputs} size={IOPanelSize}>
          <ProductEntry
            product={activeBlock[0].product}
            callback={updateRequest}
            size={IOPanelSize} />
        </OutputGroup>
        <InputGroup
          content={activeBlock.IO.inputs}
          tooltip={dict["DeployRequestToUnit"]}
          callback={deployRequest}
          size={IOPanelSize} />
      </div>
      <UnitList>
        {activeBlock.map(unit =>
          <UnitList.Item unit={unit} key={getUniqueID(unit)} />)}
      </UnitList>
    </div>
  );
}