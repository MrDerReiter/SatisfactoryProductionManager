using FactoryManagementCore;
using System.Windows;

namespace SatisfactoryProductionManager;

public partial class App : Application
{
    public static ITranslator Translator { get; private set; }
    public static IFactoryKeeper FactoryKeeper { get; private set; }
    public static IRecipeProvider<SatisfactoryRecipe> RecipeProvider { get; private set; }


    public App()
    {
        try
        {
            Translator = new DefaultTranslator("Dictionary.cfg");
            RecipeProvider = new DefaultSatisfactoryRecipeProvider("Recipies.cfg");
            FactoryKeeper = new DefaultSaveLoadManager("ProductionLines.json");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка при инициализации сессии",
                MessageBoxButton.OK, MessageBoxImage.Error);

            Shutdown();
        }
    }


    public static ProductionUnit GetUnitInstance(Recipe recipe)
    {
        return new SatisfactoryProductionUnit
            (recipe.Outputs[0].Variate(0), (SatisfactoryRecipe)recipe);
    }

    public static ProductionUnit GetUnitInstance(ResourceStream request, Recipe recipe)
    {
        return new SatisfactoryProductionUnit(request, (SatisfactoryRecipe)recipe);
    }
}
