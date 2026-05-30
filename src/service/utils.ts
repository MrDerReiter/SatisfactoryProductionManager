import { externals as errorDialog } from "../components/dialogs/ErrorMessage";


export interface IO {
  inputs: Record<string, number>;
  outputs: Record<string, number>;
}
export const formatter = new Intl.NumberFormat("en-US", {
  minimumFractionDigits: 0,
  maximumFractionDigits: 3,
  useGrouping: false
});

const operatorPattern = /[-+*/]/;
const numberPattern = /\d+(\.\d+)?/;
const expressionPattern = /\d+(\.\d+)?\s*[-+*/]\s*\d+(\.\d+)?/


export function mergeIO(first: IO, second: IO): IO {
  const result = {
    inputs: Object.assign({}, first.inputs),
    outputs: Object.assign({}, first.outputs)
  };

  for (const prop in result) {
    for (const res in second[prop]) {
      if (res in result[prop]) result[prop][res] += second[prop][res];
      else result[prop][res] = second[prop][res];
    };
  }

  return result;
}

export function balanceIO(IO: IO): IO {
  const result = {
    inputs: Object.assign({}, IO.inputs),
    outputs: Object.assign({}, IO.outputs)
  };

  for (const res in result.outputs) {
    if (!(res in result.inputs)) continue;

    //Для проверки на равенство с поправкой на погрешность float
    const delta = Math.abs(result.outputs[res] - result.inputs[res]);
    switch (true) {
      case delta < 0.001:
        delete result.outputs[res];
        delete result.inputs[res]; break;

      case result.outputs[res] > result.inputs[res]:
        delete result.inputs[res];
        result.outputs[res] = delta; break;

      default:
        delete result.outputs[res];
        result.inputs[res] = delta; break;
    }
  };

  return result;
};

export function parseInputValue(value: string): number {
  const expression = value.match(expressionPattern)?.[0];
  if (!expression) {
    const result = value.match(numberPattern)?.[0];
    return result ? Number(result) : null;
  }

  const operator = expression.match(operatorPattern)[0];
  const values = expression.split(operator).map(value => Number(value.trim()));
  if (operator == "/" && values[1] == 0) {
    handleError(new Error("Делить на ноль нельзя"));
    return null;
  }

  switch (operator) {
    case "+": return values[0] + values[1];
    case "-": return values[0] - values[1];
    case "*": return values[0] * values[1];
    case "/": return values[0] / values[1];
  }
}

export function handleError(err: Error) {
  errorDialog.showErrorMessage(err.message);
}