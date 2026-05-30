import { flushSync } from "react-dom";
import {
  useEffect,
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
  show?: (event: MouseEvent, tooltipProps: T) => void;
  /**
   * Скрывает подсказку, с небольшой задержкой (чтобы избежать "мерцания" при
   * быстрых переходах между элементами).
   * Как правило, нужно вызвать при уходе мыши с активного компонента.
   */
  hide?: VoidFunction;
  /** Служебная переменная, во внешнем коде не используется. */
  hidingTimeout?: number;
}
interface TooltipProviderProps<T> {
  /** CSS-класс для контейнера. */
  containerClass?: string;
  /** CSS-класс для всплывающей подсказки. */
  tooltipClass?: string;
  children: ReactNode;
  /**
   * Объект (как правило, изначально пустой), который должен предоставить внешний код.
   * В него будут записаны методы show() и hide(),
   * позволяющие управлять появлением и исчезновением подсказки.
   */
  dispatcher: TooltipDispatcher<T>
  /**
   * Функциональный компонент, который будет использован
   * для отрисовки содержимого контекстной подсказки.
   */
  tooltipContent: (props: T) => ReactNode;
}
interface TooltipProps {
  className?: string;
  children: ReactNode;
  visible: boolean;
  position: Position;
  elementRef?: Ref<HTMLDivElement>
}
interface Position { left: number; top: number; }


const tooltipStyle = (visible: boolean, position: Position): CSSProperties => ({
  display: visible ? "block" : "none",
  position: "fixed", ...position
});

function Tooltip(props: TooltipProps) {
  const { className, children, visible, position, elementRef } = props;
  return (
    <div style={tooltipStyle(visible, position)}
      className={className}
      ref={elementRef}>
      {children}
    </div>
  );
}


/**
 * Специальный контейнер, упрощающий отображение универсальных однотипных
 * всплывающих подсказок для каждого вложенного элемента. Управление отображением
 * подсказки происходит с помощью объекта-"пульта", который передаётся
 * вызывающим кодом (в этот обьект будут записаны соответствующие методы).
 * Затем можно привязать методы "пульта" к событиям mouseenter и mouseleave
 * вложенных элементов контейнера (их сигнатуры адаптированы для этого), так что при
 * каждом наведении/уходе курсора мыши будет отображаться в качестве 
 * всплывающей подсказки переданный с пропсами функциональный компонент
 * (в него далее можно пробросить пропcы через метод "пульта" show())
 */
export default function TooltipProvider<T = void>(props: TooltipProviderProps<T>) {
  function assignDispatcher() {
    dispatcher.hide = () =>
      dispatcher.hidingTimeout = setTimeout(() => setVisible(false), 200);

    dispatcher.show = (event, props) => {
      clearTimeout(dispatcher.hidingTimeout);
      flushSync(() => {
        setTooltipProps(props);
        setVisible(true);
      });

      const targetRect = (event.target as Element).getBoundingClientRect();
      const tooltipRect = tooltipElement.current.getBoundingClientRect();
      setPosition(calcPosition(targetRect, tooltipRect));
    };
  }

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


  const { children, containerClass, tooltipClass, dispatcher, tooltipContent } = props;
  const [visible, setVisible] = useState(false);
  const [position, setPosition] = useState<Position>({ left: 0, top: 0 });
  const [tooltipProps, setTooltipProps] = useState<T>();
  const tooltipElement = useRef<HTMLDivElement>(null);

  useEffect(assignDispatcher, []);

  return (
    <div className={containerClass}>
      {children}
      {tooltipProps &&
        <Tooltip {...{ visible, position }}
          className={tooltipClass}
          elementRef={tooltipElement}>
          {tooltipContent(tooltipProps)}
        </Tooltip>}
    </div>
  );
}