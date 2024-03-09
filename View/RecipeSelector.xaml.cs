using SatisfactoryProductionManager.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace SatisfactoryProductionManager.View
{
    /// <summary>
    /// Логика взаимодействия для RecipeSelector.xaml
    /// </summary>
    public partial class RecipeSelector : Window
    {
        public RecipeSelector()
        {
            InitializeComponent();

            var context = DataContext as RecipeSelectorVM;
            context.RecipeSelected += (recipe) => Close();

            KeyDown += (sender, args) => { if (args.Key == Key.Escape) Close(); };
        }
    }
}
