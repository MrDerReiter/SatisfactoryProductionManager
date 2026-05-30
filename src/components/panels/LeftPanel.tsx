import { useEffect, useState } from "react";
import { state, onChange } from "../../models/Factory";
import { getUniqueID } from "../../service/IDGen";
import ProductionButton from "../buttons/ProductionButton";


export default function LeftPanel() {
  const [activeLine, setActiveLine] = useState(state.activeLine);
  const [activeBlock, setActiveBlock] = useState(state.activeBlock);

  useEffect(() => {
    onChange("activeLine", setActiveLine);
    onChange("activeBlock", setActiveBlock);
  }, []);

  return (
    <aside className="stack-panel-v main-panel left-panel">
      {activeLine?.map(block =>
        <ProductionButton
          key={getUniqueID(block)}
          product={block[0].product[0]}
          isActive={block == activeBlock}
          callback={() => state.activeBlock = block}
          size={35} />)}
    </aside>
  );
}