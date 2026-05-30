import StackGroup from "../containers/StackGroup";
import RequestButton from "../buttons/RequestButton";
import locale from "../../data/locale";


interface InputGroupProps {
  size: number;
  tooltip?: string;
  content: Record<string, number>;
  callback: (request: [string, number]) => void;
}

export default function InputGroup(props: InputGroupProps) {
  const { content, size, tooltip, callback } = props;
  return (
    <StackGroup title={locale.current["Input"]}>
      {Object.entries(content).map(request =>
        <RequestButton
          {...{ request, size, tooltip, callback }}
          key={request.join(" ")} />)}
    </StackGroup>
  );
}