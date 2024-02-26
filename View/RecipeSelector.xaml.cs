using SatisfactoryProductionManager.ViewModel;
using System.Windows;

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
        }
    }
}
