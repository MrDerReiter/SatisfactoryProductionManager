using FactoryManagementCore.Elements;
using SatisfactoryProductionManager.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace SatisfactoryProductionManager.View
{
    public partial class RequestRecipeSelector : Window
    {
        public RequestRecipeSelector(ResourceRequest request)
        {
            InitializeComponent();
            var context = new RequestRecipeSelectorVM(request);
            DataContext = context;

            context.RecipeSelected += (request, recipe) => Close();

            KeyDown += (sender, args) => { if (args.Key == Key.Escape) Close(); };
        }
    }
}
