using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.ViewModel;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.Windows;
using System.Windows.Controls;

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
