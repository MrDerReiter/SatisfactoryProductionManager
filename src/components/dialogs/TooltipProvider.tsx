import { flushSync } from "react-dom";
import {
  useImperativeHandle,
  useRef,
  useState,
  type CSSProperties,
  type MouseEvent,
  type ReactNode,
  type Ref
} from "react";


export interface TooltipDispatcher<T> {
  /**
   * Отображает контекстную подсказку. Принимает в качестве параметра обьект события,
   * инициировавшего показ (как правило, событие наведения мыши на элемент) и
   * пропсы, которые будут переданы в функциональный компонент, отрисованный в подсказке.
   * @param event Событие мыши (требуется, чтобы вычислить координаты для отображения подсказки).
   * @param tooltipProps Пропсы для компонента содержимого подсказки.
   */
  show(event: MouseEvent, tooltipProps: T): void;
  /**
   * Скрывает подсказку, с небольшой задержкой (чтобы избежать "мерцания" при
   * быстрых переходах между элементами).
   * Как правило, нужно вызвать при уходе мыши с активного компонента.
   */
  hide(): void;
  /** Служебная переменная, во внешнем коде не используется. */
  hidingTimeout?: ReturnType<typeof setTimeout>;
}
interface TooltipProviderProps<T> {
  className?: string;
  children: ReactNode;
  dispatcherRef: Ref<TooltipDispatcher<T>>
  /**
   * Функциональный компонент, который будет использован
   * для отрисовки содержимого контекстной подсказки.
   */
  tooltipContent: (props: T) => ReactNode;
}
interface TooltipWrapperProps {
  children: ReactNode;
  visible: boolean;
  position: Position;
  elementRef?: Ref<HTMLDivElement>
}
interface Position { left: number; top: number; }


const wrapperStyle = (visible: boolean, position: Position): CSSProperties => ({
  display: visible ? "block" : "none",
  position: "fixed", ...position
});

function calcPosition(targetRect: DOMRect, tooltipRect: DOMRect): Position {
  const thresholdX = document.documentElement.clientWidth - tooltipRect.width;
  const thresholdY = document.documentElement.clientHeight - tooltipRect.height;
  const desiredX = targetRect.right + targetRect.width / 2;
  const fallbackX = targetRect.left - tooltipRect.width - targetRect.width / 2;

  return {
    left: desiredX < thresholdX ? desiredX : fallbackX,
    top: Math.min(targetRect.top, thresholdY)
  }
}

function Tooltip(props: TooltipWrapperProps) {
  const { children, visible, position, elementRef } = props;
  return (
    <div style={wrapperStyle(visible, position)}
      ref={elementRef}>
      {children}
    </div>
  );
}


/**
 * Предоставляет обобщённую всплывающую подсказку для вложенных элементов.
 * Подсказка представляет собой единый элемент, который разделяют все дочерние
 * элементы контейнера; меняется лишь её содержимое и позиция на экране,
 * в зависимости от конкретного элемента на котором сработало событие наведения).
 * Управление показом и скрытием подсказки осуществляется через ref-объект,
 * который передаётся через ссылку в родительский компонент (пропс dispatcherRef).
 */
export default function TooltipProvider<T>(props: TooltipProviderProps<T>) {
  const { className, children, dispatcherRef, tooltipContent } = props;
  const [visible, setVisible] = useState(false);
  const [position, setPosition] = useState<Position>({ left: 0, top: 0 });
  const [tooltipProps, setTooltipProps] = useState<T>();
  const tooltipElement = useRef<HTMLDivElement>(null);

  useImperativeHandle(dispatcherRef, () => ({
    hide() { this.hidingTimeout = setTimeout(() => setVisible(false), 200); },
    show(event, props) {
      clearTimeout(this.hidingTimeout);
      flushSync(() => {
        setTooltipProps(props);
        setVisible(true);
      });

      const targetRect = (event.target as HTMLElement).getBoundingClientRect();
      const tooltipRect = tooltipElement.current.getBoundingClientRect();
      setPosition(calcPosition(targetRect, tooltipRect));
    }
  }), []);

  return (
    <div className={className}>
      {children}
      {tooltipProps &&
        <Tooltip {...{ visible, position }}
          elementRef={tooltipElement}>
          {tooltipContent(tooltipProps)}
        </Tooltip>}
    </div>
  );
};