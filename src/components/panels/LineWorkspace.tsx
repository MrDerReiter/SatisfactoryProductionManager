import { useEffect, useState } from "react";
import { state, onChange, addProductionBlock } from "../../models/Factory";
import locale from "../../data/locale";
import ImageButton from "../buttons/ImageButton";
import InputGroup from "../controls/InputGroup";
import OutputGroup from "../controls/OutputGroup";


const blockRemover = () => state.activeBlock.remove();
const moveLeft = () => state.activeLine.move("left");
const moveRight = () => state.activeLine.move("right");
const commandButtonSize = 25;
const IOGroupSize = 30;

export default function LineWorkspace() {
  const [dict, setDict] = useState(locale.current);
  const [{ IO: { inputs, outputs } }, setActiveLine] = useState(state.activeLine);

  useEffect(() => {
    locale.onChange(setDict);
    onChange("activeLine", setActiveLine);
  }, []);

  return (
    <div className="stack-panel-h width-scrollable line-workspace">
      <div className="stack-panel-h">
        <ImageButton
          image="assets/AddItem.png"
          size={commandButtonSize}
          className="command-button"
          tooltip={dict["AddProductionBlock"]}
          callback={addProductionBlock} />
        <ImageButton
          image="assets/DeleteItem.png"
          size={commandButtonSize}
          className="command-button"
          tooltip={dict["RemoveProductionBlock"]}
          callback={blockRemover} />
      </div>
      <div className="stack-panel-h">
        <ImageButton
          image="assets/MoveLineLeft.png"
          size={commandButtonSize}
          className="command-button"
          tooltip={dict["MoveLineLeft"]}
          callback={moveLeft} />
        <ImageButton
          image="assets/MoveLineRight.png"
          size={commandButtonSize}
          className="command-button"
          tooltip={dict["MoveLineRight"]}
          callback={moveRight} />
      </div>
      <div className="stack-panel-h huge-gap">
        <OutputGroup
          content={outputs}
          size={IOGroupSize} />
        <InputGroup
          content={inputs}
          size={IOGroupSize}
          tooltip={dict["DeployRequestToBlock"]}
          callback={addProductionBlock} />
      </div>
    </div>
  );
}