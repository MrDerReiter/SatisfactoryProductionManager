import type { CSSProperties } from "react";
import { formatter } from "../../service/utils";
import locale from "../../data/locale";
import ValueEntry from "./ValueEntry";


interface ImageEntryProps {
  content: [imageSource: string, initValue: number];
  callback: (value: number) => string | null;
  colors: [string, string];
  size: number;
  tooltip?: string
}
const textBox = (color: string, size: number): CSSProperties => ({
  backgroundColor: color,
  color: "inherit",
  fontFamily: "inherit",
  fontSize: "inherit",
  width: size
});

export default function ImageEntry(props: ImageEntryProps) {
  const { content, colors, size, tooltip, callback } = props;
  return (
    <div className="stack-panel-v" style={{ backgroundColor: colors[0] }}>
      <img src={content[0]}
        title={tooltip}
        width={size}
        height={size}
        draggable={false} />
      <ValueEntry
        initValue={formatter.format(content[1])}
        style={textBox(colors[1], size + 8)}
        tooltip={locale.current["SetCount"]}
        handler={callback} />
    </div>
  );
}