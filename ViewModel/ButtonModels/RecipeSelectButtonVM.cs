using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class RecipeSelectButtonVM : ObjectButtonVM<Recipe>
    {
        public RecipeTooltipVM Tooltip { get; }

        public RecipeSelectButtonVM(Recipe recipe)
        {
            InnerObject = recipe;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{recipe.Product.Resource}.png", UriKind.Relative));
            Tooltip = new RecipeTooltipVM(recipe);
        }
    }
}
