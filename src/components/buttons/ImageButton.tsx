import type { MouseEvent } from "react";
import { playPushButtonSound } from "../../service/player";


interface ImageButtonProps {
  image: string;
  size: number;
  tooltip?: string;
  className?: string;
  onHover?: (event: MouseEvent) => void;
  onLeave?: VoidFunction;
  callback?: VoidFunction;
}

export default function ImageButton(props: ImageButtonProps) {
  const onClick = async () => { await playPushButtonSound(); callback?.(); }
  const { image, size, tooltip, className, onHover, onLeave, callback } = props;

  return (
    <button {...{ className, onClick }}
      onMouseEnter={onHover}
      onMouseLeave={onLeave}
      title={tooltip}>
      <img src={image}
        width={size}
        height={size}
        draggable={false} />
    </button>
  );
}