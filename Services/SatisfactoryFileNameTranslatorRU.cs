using FactoryManagementCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace SatisfactoryProductionManager.Services
{
    public class SatisfactoryFileNameTranslatorRU : INameTranslator
    {
        private static readonly string _dictPath = $"{Environment.CurrentDirectory}\\DictionaryRU.txt";
        private static readonly Dictionary<string, string> _dictionary = InitializeDictionary();


        private static Dictionary<string, string> InitializeDictionary()
        {
            try
            {
                var lines = File.ReadAllLines(_dictPath);
                var dict = new Dictionary<string, string>();

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#')) continue;

                    var pair = line.Split(" => ");
                    dict.Add(pair[0], pair[1]);
                }

                return dict;
            }

            catch (Exception ex)
            {
                MessageBox.Show
                    (ex.Message, "Ошибка при инициализации словаря",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                throw new InvalidDataException("Не удалось инициализировать словарь для перевода строк.");
            }
        }


        public string Translate(string name)
        {
            try { return _dictionary[name]; }
            catch (Exception ex)
            {
                MessageBox.Show
                    (ex.Message, "Ошибка при переводе строки",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                throw new InvalidDataException("Не удалось перевести строку; обнаружено некорректное поведение словаря.");
            }
        }
    }
}
