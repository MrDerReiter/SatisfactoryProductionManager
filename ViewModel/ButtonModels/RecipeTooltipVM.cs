using FactoryManagementCore.Extensions;
using SatisfactoryProductionManager.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class RecipeTooltipVM
    {
        public string Title { get; }
        public ImageTuple[] Outputs { get; }
        public ImageTuple[] Inputs { get; }


        public RecipeTooltipVM(SatisfactoryRecipe recipe)
        {
            Title = recipe.Name.Translate();

            if (recipe.HasByproduct)
            {
                Outputs =
                    [
                        new ImageTuple(recipe.Product.Resource, recipe.Product.CountPerMinute),
                        new ImageTuple(recipe.Byproduct.Value.Resource, recipe.Byproduct.Value.CountPerMinute)
                    ];
            }
            else
            {
                Outputs = [new ImageTuple(recipe.Product.Resource, recipe.Product.CountPerMinute)];
            }

            Inputs = recipe.Inputs
                .Select(stream => new ImageTuple(stream.Resource, stream.CountPerMinute)).ToArray();
        }


        public struct ImageTuple
        {
            public ImageSource Image { get; }
            public string Count { get; }

            public ImageTuple(string resource, double count)
            {
                Image = new BitmapImage(new Uri($"../Assets/Resources/{resource}.png", UriKind.Relative));
                Count = count.ToString("0.###", CultureInfo.InvariantCulture) + "/мин.";
            }
        }
    }
}
