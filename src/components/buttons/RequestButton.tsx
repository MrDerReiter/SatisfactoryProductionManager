import { playPushButtonSound } from "../../service/player";
import { formatter } from "../../service/utils";


interface RequestButtonProps {
  size: number;
  tooltip?: string;
  request: [string, number];
  callback: (request: [string, number]) => void;
}

export default function RequestButton(props: RequestButtonProps) {
  const onClick = async () => { await playPushButtonSound(); callback(request); }
  const { size, tooltip, request, callback } = props;

  return (
    <button className="request-button" title={tooltip} onClick={onClick}>
      <img src={`images/Resources/${request[0]}.png`}
        width={size}
        height={size}
        draggable={false} />
      <p>{formatter.format(request[1])}</p>
    </button>
  );
}