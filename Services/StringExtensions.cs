using SatisfactoryProductionManager.Model;

namespace SatisfactoryProductionManager.Services
{
    public static class StringExtensions
    {
        public static string TranslateRU(this string str)
        {
            return ProductionManager.NameTranslator.Translate(str);
        }
    }
}
