import { useEffect, useState } from "react";
import { state, onChange, addProductionLine } from "../../models/Factory";
import { getUniqueID } from "../../service/IDGen";
import locale from "../../data/locale";
import ImageButton from "../buttons/ImageButton";
import ProductionButton from "../buttons/ProductionButton";
import LangSelector from "../buttons/LangSelector";


const buttonSize = 35;

export default function HeaderPanel() {
  const [dict, setDict] = useState(locale.current);
  const [lines, setLines] = useState(state.productionLines);
  const [activeLine, setActiveLine] = useState(state.activeLine);

  useEffect(() => {
    locale.onChange(setDict);
    onChange("productionLines", setLines);
    onChange("activeLine", setActiveLine);
  }, []);

  return (
    <header className="stack-panel-h main-panel header-panel">
      <ImageButton
        image="assets/AddLine.png"
        tooltip={dict["AddProductionLine"]}
        callback={addProductionLine}
        size={buttonSize} />
      <div className="stack-panel-h small-gap width-scrollable">
        {lines.map(line =>
          <ProductionButton
            key={getUniqueID(line)}
            product={line[0][0].product[0]}
            isActive={line == activeLine}
            callback={() => state.activeLine = line}
            size={buttonSize} />)}
      </div>
      <LangSelector />
    </header>
  );
}