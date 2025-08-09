using FactoryManagementCore;
using System.Windows;
using System.Windows.Input;

namespace SatisfactoryProductionManager;

public partial class RequestRecipeSelector : Window
{
    public RequestRecipeSelector(ResourceStream request)
    {
        InitializeComponent();

        var context = new RequestRecipeSelectorVM(request);
        DataContext = context;

        context.RecipeSelected += (request, recipe) => Close();

        KeyDown += (sender, args) => 
        {
            if (args.Key == Key.Escape) Close();
        };
    }
}
