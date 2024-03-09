using SatisfactoryProductionManager.Model.Elements;
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
            DataContext = new RequestRecipeSelectorVM(request);

            var context = DataContext as RequestRecipeSelectorVM;
            context.RecipeSelected += (request, recipe) => Close();

            KeyDown += (sender, args) => { if (args.Key == Key.Escape) Close(); };
        }
    }
}
