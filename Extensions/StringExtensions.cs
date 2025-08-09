using FactoryManagementCore;


namespace SatisfactoryProductionManager.Extensions;

public static class StringExtensions
{
    public static string Translate(this string str)
    {
        return App.Translator.Translate(str);
    }

    public static ResourceStream ToStream(this string str)
    {
        return new ResourceStream(str);
    }
}
