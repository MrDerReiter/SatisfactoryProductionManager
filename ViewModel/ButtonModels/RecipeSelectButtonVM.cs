using Prism.Commands;
using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class RecipeSelectButtonVM : ObjectButtonVM<Recipe>
    {
        public RecipeSelectButtonVM(Recipe recipe)
        {
            InnerObject = recipe;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{recipe.Product.Resource}.png", UriKind.Relative));
        }
    }
}
