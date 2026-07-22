import type { ReactNode } from "react";

type Condition = boolean | object | null | undefined;
interface ShowProps { when: Condition, children: ReactNode }

/**
 * Универсальный контейнер, который отображает и скрывает своё содержимое по условию
 * (передаётся в пропс со свойством when). В качестве условия можно передать как boolean,
 * так и любой тип, который можно к нему привести
 * (или выражение/вызов функции, которое возвращает этот тип).
 */
export default function Show(props: ShowProps) {
  const { when, children } = props;
  return when && children;
}