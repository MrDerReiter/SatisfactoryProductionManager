import { useState, type CSSProperties, type FocusEvent } from "react";
import { parseInputValue } from "../../service/utils";


interface ValueEntryProps {
  initValue: string;
  tooltip?: string;
  className?: string;
  style?: CSSProperties;
  handler: (value: number) => string | null;
}

export default function ValueEntry(props: ValueEntryProps) {
  function inputHandler(event: FocusEvent<HTMLInputElement>) {
    const value = parseInputValue(event.target.value);
    if (!value) { setHasError(true); return; }

    setHasError(false);
    const fallbackValue = handler(value);
    if (fallbackValue) event.target.value = fallbackValue;
  }

  const { initValue, className, style, tooltip, handler } = props;
  const [hasError, setHasError] = useState(false);

  return (
    <input type="text"
      className={hasError ? `${className ?? ""} input-error` : className}
      style={style}
      title={tooltip}
      onBlur={inputHandler}
      defaultValue={initValue} />
  );
}