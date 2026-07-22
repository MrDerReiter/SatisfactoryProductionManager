import { formatter } from "../../service/utils";


interface LabelProps {
  content: [string, number];
  colors: [string, string];
  size: number;
  tooltip?: string;
}

export default function Label(props: LabelProps) {
  const { content, colors, size, tooltip } = props;
  return (
    <div className="stack-panel-v label" style={{ backgroundColor: colors[0] }}>
      <img src={`images/Resources/${content[0]}.png`}
        title={tooltip}
        width={size}
        height={size}
        draggable={false} />
      <p style={{ backgroundColor: colors[1] }}>{formatter.format(content[1])}</p>
    </div>
  );
}