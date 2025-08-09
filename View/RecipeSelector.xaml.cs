using System.Windows;
using System.Windows.Input;

namespace SatisfactoryProductionManager;

public partial class RecipeSelector : Window
{
    public RecipeSelector()
    {
        InitializeComponent();

        var context = (RecipeSelectorVM)DataContext;
        context.RecipeSelected += (recipe) => Close();

        KeyDown += (sender, args) => 
        {
            if (args.Key == Key.Escape)
                Close(); 
        };
    }
}
