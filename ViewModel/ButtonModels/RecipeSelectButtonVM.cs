using FactoryManagementCore;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager;

public partial class RecipeSelectButtonVM : ObjectButtonVM<Recipe>
{
    public RecipeTooltipVM Tooltip { get; }


    public RecipeSelectButtonVM(SatisfactoryRecipe recipe)
    {
        string imagePath = recipe.Category == "PowerGenerating" ?
            $"../Assets/Resources/{recipe.Inputs[0].Resource}.png" :
            $"../Assets/Resources/{recipe.Product.Resource}.png";

        InnerObject = recipe;
        Image = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        Tooltip = new RecipeTooltipVM(recipe);
    }


    protected override void SelectObject()
    {
        try { base.SelectObject(); }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка при инициализации выбора рецепта",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
