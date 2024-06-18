using FactoryManagementCore.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SatisfactoryProductionManager.Services
{
    public class SatisfactoryFileNameTranslator : INameTranslator
    {
        private readonly Dictionary<string, string> _dict = InitializeDictionary("Dictionary.cfg");


        private static Dictionary<string, string> InitializeDictionary(string dictPath)
        {
            var dict = new Dictionary<string, string>();
            var entriesToAdd = File.ReadAllLines(dictPath, Encoding.Unicode);

            foreach ( var entry in entriesToAdd )
            {
                if (!entry.StartsWith('#') && !string.IsNullOrEmpty(entry))
                {
                    var pair = entry.Split(" => ");
                    dict.Add(pair[0], pair[1]);
                }
            }

            return dict;
        }

        public string Translate(string name)
        {
            return _dict[name];
        }
    }
}
