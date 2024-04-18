using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryProductionManager.Services
{
    public static class StringExtensions
    {
        public static string TranslateRU(this string str)
        {
            return NameTranslatorRU.Translate(str);
        }
    }
}
