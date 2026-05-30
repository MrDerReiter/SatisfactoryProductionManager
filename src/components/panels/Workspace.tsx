import { useEffect, useState, type PropsWithChildren } from "react";
import { state, onChange } from "../../models/Factory";
import type ProductionLine from "../../models/ProductionLine";


export default function Workspace({ children }: PropsWithChildren) {
  const [isLineSelected, setIsLineSelected] = useState(Boolean(state.activeLine));
  useEffect(() => onChange("activeLine", (line: ProductionLine) => setIsLineSelected(Boolean(line))), []);

  return (
    <main
      className="stack-panel-v main-panel workspace"
      hidden={!isLineSelected}>
      {isLineSelected && children}
    </main>
  );
}