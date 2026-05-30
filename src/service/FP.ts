const mutationError = new Error("Мутирующие методы массива недопустимы");
function emitOnChange<L extends ImmutableList<T>, T>(sender: L, newState: L): L {
  sender.onChange?.(newState);
  return newState;
}


export class ImmutableList<T> extends Array<T> {
  declare onChange?: (newState: ImmutableList<T>) => void;

  constructor(...args: T[]) { super(...args); }
  static override from<T>(arr: ArrayLike<T> | Iterable<T>) { return new this(...Array.from(arr)); }


  append(...args: T[]) {
    return emitOnChange(this,
      new (this.constructor as new (...args: any[]) => this)(...this, ...args));
  }

  cut(child: T) {
    return emitOnChange(this,
      this.filter(item => item != child) as this);
  }

  replace(prevChild: T, newChild: T) {
    return emitOnChange(this, this.map(item => {
      if (item == prevChild) return newChild;
      else return item;
    }) as this);
  }

  swap(left: T, right: T) {
    return emitOnChange(this, this.map(item =>
      item == left ? right : item == right ? left : item) as unknown as ImmutableList<T>);
  }

  //Блокировка мутирующих методов массива
  override pop(): never { throw mutationError; }
  override push(): never { throw mutationError; }
  override shift(): never { throw mutationError; }
  override unshift(): never { throw mutationError; }
  override splice(): never { throw mutationError; }
}

export class Functor<T> {
  readonly value: T
  constructor(value: T) { this.value = value; }
  static of<T>(value: T) { return new Functor(value); }
  map<R>(mapper: (value: T) => R) { return new Functor(mapper(this.value)); }
  join<R>(consumer: (value: T) => R) { return consumer(this.value); }
}

export abstract class Either<T> {
  readonly value: T;
  constructor(value: T) { this.value = value }
  static left(value: Error) { return new Left(value); }
  static right<T>(value: T) { return new Right(value); }
  static try<T>(action: () => T): Either<T | Error> {
    try { return Either.right(action()); }
    catch (error) { return Either.left(error as Error); }
  }

  abstract map<R>(mapper: (value: T) => R): Either<R | Error>;
  abstract chain<R>(wrapper: (value: T) => Either<R>): Either<R | Error>;
  abstract fold(onFail: (error: Error) => void, onSuccess: (value: T) => void): void;
}

class Left extends Either<Error> {
  map(): Either<Error> { return this; }
  chain(): Either<Error> { return this; }
  fold(onFail: (error: Error) => void, onSuccess: (value: any) => void) { onFail(this.value); }
}

class Right<T> extends Either<T> {
  map<R>(mapper: (value: T) => R): Either<R> { return Either.right(mapper(this.value)); }
  chain<R>(wrapper: (value: T) => Either<R>): Either<R> { return wrapper(this.value); }
  fold(onFail: (value: any) => void, onSuccess: (value: T) => void) { onSuccess(this.value); }
}