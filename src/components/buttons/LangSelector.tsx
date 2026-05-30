import locale from "../../data/locale";


export default function LangSelector() {
  return (
    <select
      className="lang-selector"
      defaultValue={locale.localeID}
      onChange={event => locale.set(event.target.value)}>
      <option value="ENG">ENG</option>
      <option value="RU">RU</option>
    </select>
  );
}