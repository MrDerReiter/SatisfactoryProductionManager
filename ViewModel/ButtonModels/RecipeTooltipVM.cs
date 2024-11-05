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
                if (recipe.Category == "PowerGenerating")
                    Outputs = [
                            new ImageTuple(recipe.Product.Resource, recipe.Product.CountPerMinute, true),
                            new ImageTuple(recipe.Byproduct.Value.Resource, recipe.Byproduct.Value.CountPerMinute)
                        ];

                else Outputs = [
                            new ImageTuple(recipe.Product.Resource, recipe.Product.CountPerMinute),
                            new ImageTuple(recipe.Byproduct.Value.Resource, recipe.Byproduct.Value.CountPerMinute) 
                        ];
            }

            else if (recipe.Category == "PowerGenerating")
                Outputs = [new ImageTuple(recipe.Product.Resource, recipe.Product.CountPerMinute, true)];

            else Outputs = [new ImageTuple(recipe.Product.Resource, recipe.Product.CountPerMinute)];

            Inputs = recipe.Inputs
                .Select(stream => new ImageTuple(stream.Resource, stream.CountPerMinute)).ToArray();
        }


        public readonly struct ImageTuple(string resource, double count, bool isOutputPower = false)
        {
            public ImageSource Image { get; } = new BitmapImage(new Uri($"../Assets/Resources/{resource}.png", UriKind.Relative));
            public string Count { get; } = isOutputPower ?
                count.ToString("0.###", CultureInfo.InvariantCulture) + " MW" :
                count.ToString("0.###", CultureInfo.InvariantCulture) + "/мин.";
        }
    }
}
