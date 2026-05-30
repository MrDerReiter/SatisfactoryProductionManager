import type { ReactNode } from "react";


export default function StackGroup(props: { title: string, children?: ReactNode }) {
  const { title, children } = props;
  return (
    <div className="stack-panel-h stack-group">
      <p>{title}</p>
      <div className="stack-panel-h">{children}</div>
    </div>
  );
}