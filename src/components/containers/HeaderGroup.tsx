import type { ReactNode } from "react";


export default function HeaderGroup(props: { headerTitle: string, children?: ReactNode }) {
  const { headerTitle, children } = props;
  return (
    <div className="stack-panel-v header-group">
      <p>{headerTitle}</p>
      <div className="stack-panel-h">{children}</div>
    </div>
  );
}