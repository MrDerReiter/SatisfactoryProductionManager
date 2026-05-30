import { useEffect, useRef, type ReactNode } from "react";

type Executor<T> = ConstructorParameters<typeof Promise<T>>[0];
interface ModalProps<T> {
  children: ReactNode;
  /**
   * Объект (как правило, изначально пустой), передаваемый в пропс клиентским кодом;
   * после инициализации компонента Modal в этот объект будут записаны методы open() и cancel(),
   * позволяющие управлять диалоговым окном.
   */
  caller: DialogCaller<T>;
  /**
   * CSS-классы, которые будут применены к элементу диалога.
   * В большинстве случаев в этом нет необходимости, т.к. проще стилизовать
   * непосредственно содержимое модального окна, а не само окно.
   */
  className?: string;
}
export interface DialogCaller<T> {
  /**
   * Открывает диалоговое окно. Сигнатура метода идентична сигнатуре
   * конструктора new Promise(), он принимает такую-же функцию в качестве параметра и
   * возвращает соответствующий промис, который резрешится при вызове resolve(value)
   * со значением value и автоматически закроет диалоговое окно.
   * Однако этот промис может также разрешиться с пустым значением ещё до вызова
   * resolve(), если окно было закрыто принудительно
   * (пользователь нажал Esc или был вызван метод обьекта caller.cancel()).
   * @param executor
   * Функция-исполнитель, со стандартной сигнатурой параметра конструктора промиса:
   * (resolve, reject) => void
   * @returns Промис, который будет разрешён при закрытии диалогового окна;
   * либо со значением, переданным в вызов resolve(), либо с пустым значением,
   * если диалог был закрыт принудительно.
   */
  open?: (executor: Executor<T>) => Promise<T | void>;
  /**
   * Немедленно закрывает диалоговое окно и разрешает промис,
   * возвращённый методом caller.open() с пустым значением.
   * Идентично нажатию пользователем клавиши Esc (можно использовать для
   * реализации кнопки закрытия внутри диалогового окна);
   */
  cancel?: () => void;
}

let forceClosing: VoidFunction;
const onCancel = () => forceClosing();


/**
 * Универсальное диалоговое окно. Отображает любое вложенное в него содержимое в
 * отдельном модальном окне, которое изначально скрыто. Отображение и сокрытие
 * окна осуществляется с помощью объекта-"пульта", который передаётся из вызывающего
 * кода пропсом, и в него записываются соответствующие методы.
 * Диалог может возращать результат с помощью промиса, который в свою очередь
 * возвращается методом открытия окна
 * (тип этого значения определяется generic-параметром модального окна).
 */
export default function Modal<TResult = void>(props: ModalProps<TResult>) {
  function dialogHandler(executor: Executor<TResult>): Promise<TResult | void> {
    const canceled = new Promise<void>(resolve => forceClosing = resolve);
    const isDone = new Promise<TResult>(executor)
      .finally(() => dialog.current.close());

    dialog.current.showModal();
    return Promise.race([isDone, canceled]);
  }

  const { children, caller, className } = props;
  const dialog = useRef<HTMLDialogElement>(null);

  useEffect(() => {
    caller.open = dialogHandler;
    caller.cancel = () => dialog.current.requestClose();
  }, []);

  return (
    <dialog {...{ onCancel, className }}
      ref={dialog}>
      {children}
    </dialog>
  );
}