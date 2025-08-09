using FactoryManagementCore;
using FactoryManagementCore.Extensions;
using SatisfactoryProductionManager.Extensions;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SatisfactoryProductionManager;

public class RecipeSelectorVM
{
    public class CategoryVM
    {
        public required string Name { get; init; }
        public required string ViewName { get; init; }
        public required ImageSource Image { get; init; }
        public required List<RecipeSelectButtonVM> RecipeButtons { get; init; }
    }


    private readonly Dictionary<string, string> _categoryAssets = new()
    {
        { "Ingots", "../Assets/Resources/IronIngot.png"},
        { "Minerals", "../Assets/Resources/Concrete.png" },
        { "StandartParts", "../Assets/Resources/ModularFrame.png" },
        { "IndustrialParts", "../Assets/Resources/Motor.png" },
        { "Electronics", "../Assets/Resources/Cable.png" },
        { "Communications", "../Assets/Resources/Computer.png" },
        { "QuantumTech", "../Assets/Resources/SingularityCell.png" },
        { "SpaceElevatorParts", "../Assets/Resources/AssemblyDirectorSystem.png" },
        { "Converting", "../Assets/Resources/SAM.png" },
        { "Supplies", "../Assets/Resources/RifleAmmo.png" },
        { "Liquids", "../Assets/Resources/Fuel.png" },
        { "Packages", "../Assets/Resources/EmptyCanister.png" },
        { "Burnable", "../Assets/Resources/SolidBiofuel.png" },
        { "Nuclear", "../Assets/Resources/UraniumFuelRod.png" },
        { "PowerGenerating", "../Assets/Resources/Power.png" }
    };

    public List<CategoryVM> Categories { get; }

    public event Action<Recipe> RecipeSelected;


    public RecipeSelectorVM()
    {
        var list = new List<CategoryVM>(_categoryAssets.Count);

        _categoryAssets.ForEach(pair =>
        {
            var name = pair.Key;
            var filePath = pair.Value;

            list.Add(new CategoryVM
            {
                Name = name,
                ViewName = name.Translate(),
                Image = new BitmapImage(new Uri(filePath, UriKind.Relative)),
                RecipeButtons = CreateButtonsCollection(name)
            });
        });

        Categories = list;
    }


    private List<RecipeSelectButtonVM> CreateButtonsCollection(string category)
    {
        var list = App.RecipeProvider
            .GetSubset(recipe => recipe.Category == category)
            .Select(recipe => new RecipeSelectButtonVM(recipe))
            .ToList();

        list.ForEach((button) =>
        {
            button.ObjectSelected +=
                recipe => RecipeSelected(recipe);
        });
        return list;
    }
}
