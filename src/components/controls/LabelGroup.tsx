import locale from "../../data/locale";
import Label from "./Label";
import HeaderGroup from "../containers/HeaderGroup";

interface LabelGroupProps {
  title: string;
  size: number;
  colors: [string, string];
  values: [string, number][];
}

export default function LabelGroup(props: LabelGroupProps) {
  const { title, colors, values, size } = props;
  return (
    <HeaderGroup headerTitle={title}>
      {values.map(entry =>
        <Label {...{ size, colors }}
          content={entry}
          tooltip={locale.current[entry[0]]}
          key={entry.join(" ")} />)}
    </HeaderGroup>
  );
}