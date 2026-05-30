import type { ReactNode } from "react";
import ImageButton from "../buttons/ImageButton";


export interface ButtonSchema {
  image: string;
  tooltip?: string;
  specialClass?: string;
  callback: VoidFunction;
}
interface ToolbarProps {
  size: number;
  buttons: ButtonSchema[];
  buttonsClass: string;
  children?: ReactNode;
}


export default function VerticalToolbar(props: ToolbarProps) {
  const { size, buttonsClass, buttons, children } = props;
  return (
    <div className="stack-panel-v toolbar">
      {buttons.map(button =>
        <ImageButton className={buttonsClass + " " + (button.specialClass ?? "")}
          image={button.image}
          tooltip={button.tooltip}
          callback={button.callback}
          key={button.image}
          size={size} />)}
      {children}
    </div>
  );
}