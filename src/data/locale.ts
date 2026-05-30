class LocaleProvider {
  private _id: string;
  private _current: Record<string, string>;
  private _listeners: ((dict: Record<string, string>) => void)[] = [];


  constructor(initID: string, initLocale: Record<string, string>) {
    this._id = initID;
    this._current = initLocale;
  }

  get current() { return this._current; };
  get localeID() { return this._id; };

  async set(id: string) {
    if (id == this._id) return;

    this._id = id;
    localStorage.setItem("localeID", id);

    this._current = await import(/* @vite-ignore */`./dict${id}.js`).then(module => module.default);
    this._listeners.forEach(callback => callback(this._current));
  }

  onChange(callback: (dict: Record<string, string>) => void) {
    this._listeners.push(callback);
  }
}

const id = localStorage.getItem("localeID") ?? "ENG";
const locale = await import(/* @vite-ignore */`./dict${id}.js`).then(module => module.default);

export default new LocaleProvider(id, locale);