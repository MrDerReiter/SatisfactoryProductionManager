using SatisfactoryProductionManager.Extensions;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SatisfactoryProductionManager;

public class RecipeTooltipVM
{
    public readonly struct ImageTuple(string resource, double count, bool isOutputPower = false)
    {
        public ImageSource Image { get; } = new BitmapImage
            (new Uri($"../Assets/Resources/{resource}.png", UriKind.Relative));

        public string FormattedCount { get; } = 
            count.ToString("0.###", CultureInfo.InvariantCulture) +
            (isOutputPower ? " MW" : "/мин.");
    }


    public string Title { get; }
    public ImageTuple[] Inputs { get; }
    public ImageTuple[] Outputs { get; }


    public RecipeTooltipVM(SatisfactoryRecipe recipe)
    {
        string product = recipe.Product.Resource;
        double productCount = recipe.Product.CountPerMinute;
        bool isOutputPower = recipe.Category == "PowerGenerating";

        Title = recipe.Name.Translate();
        Inputs = recipe.Inputs
            .Select(stream => new ImageTuple(stream.Resource, stream.CountPerMinute))
            .ToArray();

        Outputs = new ImageTuple[recipe.Outputs.Length];
        Outputs[0] = new ImageTuple(product, productCount, isOutputPower);

        if (recipe.HasByproducts)
        {
            string byproduct = recipe.Byproduct.Resource;
            double byproductCount = recipe.Byproduct.CountPerMinute;

            Outputs[1] = new ImageTuple(byproduct, byproductCount, isOutputPower);
        }
    }
}
