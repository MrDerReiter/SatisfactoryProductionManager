using FactoryManagementCore;
using System.IO;

namespace SatisfactoryProductionManager
{
    /// <summary>
    /// Базовая реализация интерфейса INameTranslator,
    /// использующая словарь в формате текстового файла.
    /// </summary>
    public class DefaultTranslator(string dictFilePath) : ITranslator
    {
        private readonly Dictionary<string, string> _dict =
            InitializeDictionary(dictFilePath);


        private static Dictionary<string, string> InitializeDictionary(string dictFilePath)
        {
            var dict = new Dictionary<string, string>();
            var entries = File.ReadAllLines(dictFilePath);

            try
            {
                foreach (var entry in entries)
                {
                    if (string.IsNullOrEmpty(entry) || entry.StartsWith('#')) continue;

                    var pair = entry.Split(" => ");
                    dict[pair[0]] = pair[1];
                }

                return dict;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException
                    ("Ошибка при построении словаря для перевода строк. " +
                    $"Проверьте корректность данных в текстовом файле {dictFilePath}", ex);
            }
        }


        /// <summary>
        /// Переводит строку названия станка, рецепта или ресурса из внутреннего программного представления
        /// в представление для показа в GUI
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Translate(string name)
        {
            try { return _dict[name]; }
            catch (Exception ex)
            {
                throw new InvalidDataException
                    ($"Не удалось найти перевод для строки {name} в словаре. " +
                      "Проверьте наличие данного слова в исходном словаре в файле " +
                     $"{dictFilePath}", ex);
            }
        }
    }
}
