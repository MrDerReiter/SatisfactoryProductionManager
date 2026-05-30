import { type ReactElement } from "react";
import type ProductEntry from "./ProductEntry";
import locale from "../../data/locale";
import StackGroup from "../containers/StackGroup";
import Label from "./Label";


interface OutputGroupProps {
  size: number;
  content: Record<string, number>;
  children?: ReactElement<Parameters<typeof ProductEntry>[0], typeof ProductEntry>;
}

export default function OutputGroup(props: OutputGroupProps) {
  const { content, size, children } = props;
  const entries = children ?
    Object.entries(content).slice(1) :
    Object.entries(content);

  return (
    <StackGroup title={locale.current["Output"]}>
      {children}
      {entries.map(content =>
        <Label {...{ content, size }}
          colors={["lightblue", "cadetblue"]}
          key={content.join(" ")} />)}
    </StackGroup>
  );
}