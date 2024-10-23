using SatisfactoryProductionManager.Model;
using System;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class RecipeSelectButtonVM : ObjectButtonVM<SatisfactoryRecipe>
    {
        public RecipeTooltipVM Tooltip { get; }

        public RecipeSelectButtonVM(SatisfactoryRecipe recipe)
        {
            InnerObject = recipe;
            ImageSource = recipe.Category == "PowerGenerating" ?
                new BitmapImage(new Uri($"../Assets/Resources/{recipe.Inputs[0].Resource}.png", UriKind.Relative)) :
                new BitmapImage(new Uri($"../Assets/Resources/{recipe.Product.Resource}.png", UriKind.Relative));
            Tooltip = new RecipeTooltipVM(recipe);
        }
    }
}
