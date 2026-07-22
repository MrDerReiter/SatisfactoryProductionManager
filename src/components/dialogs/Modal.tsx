import {
  useImperativeHandle,
  useRef,
  type Ref,
  type ReactNode
} from "react";


type Executor<T> = ConstructorParameters<typeof Promise<T>>[0];
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
  open(executor: Executor<T>): Promise<T | void>;
  /**
   * Немедленно закрывает диалоговое окно и разрешает промис,
   * возвращённый методом caller.open() с пустым значением.
   * Идентично нажатию пользователем клавиши Esc (можно использовать для
   * реализации кнопки закрытия внутри диалогового окна);
   */
  cancel(): void;
}
interface ModalProps<T> {
  children: ReactNode;
  className?: string;
  callerRef: Ref<DialogCaller<T>>
}
let forceClosing: VoidFunction;

/**
 * Универсальное диалоговое окно. Отображает любое помещённое в него содержимое
 * в отдельном окне поверх остального интерфейса и в отдельном визуальном дереве.
 * Управление скрытием и показом диалога происходит посредством ref-объекта,
 * который передаётся через ссылку в родительский компонент (пропс callerRef).
 * Диалог так-же может возвращать значение при штатном закрытии
 * через интерфейс промиса.
 */
export default function Modal<T = void>(props: ModalProps<T>) {
  function dialogHandler(executor: Executor<T>): Promise<T | void> {
    const canceled = new Promise<void>(resolve => forceClosing = resolve);
    const isDone = new Promise<T>(executor).finally(() => dialog.current.close());

    dialog.current.showModal();
    return Promise.race([isDone, canceled]);
  }

  const { children, className, callerRef } = props;
  const dialog = useRef<HTMLDialogElement>(null);

  useImperativeHandle(callerRef, () => ({
    open: dialogHandler,
    cancel: () => dialog.current.requestClose()
  }), []);

  return (
    <dialog ref={dialog}
      className={className}
      onCancel={() => forceClosing()}>
      {children}
    </dialog>
  );
};