using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SatisfactoryProductionManager.ViewModel
{
    public class RecipeSelectorVM
    {
        public List<RecipeSelectButtonVM> IngotsButtons { get; private set; }
        public List<RecipeSelectButtonVM> MineralsButtons { get; private set; }
        public List<RecipeSelectButtonVM> StandartPartsButtons { get; private set; }
        public List<RecipeSelectButtonVM> IndustrialPartsButtons { get; private set; }
        public List<RecipeSelectButtonVM> ElectronicsButtons { get; private set; }
        public List<RecipeSelectButtonVM> CommunicationsButtons { get; private set; }
        public List<RecipeSelectButtonVM> SpaceElevatorPartsButtons { get; private set; }
        public List<RecipeSelectButtonVM> SuppliesButtons { get; private set; }
        public List<RecipeSelectButtonVM> LiquidsButtons { get; private set; }
        public List<RecipeSelectButtonVM> PackagesButtons { get; private set; }
        public List<RecipeSelectButtonVM> BurnableButtons { get; private set; }
        public List<RecipeSelectButtonVM> NuclearButtons { get; private set; }

        public event Action<SatisfactoryRecipe> RecipeSelected;


        public RecipeSelectorVM()
        {
            var properties =
                 GetType()
                .GetProperties()
                .Where(prop => prop.PropertyType == typeof(List<RecipeSelectButtonVM>));

            foreach (var prop in properties)
            {
                var category = prop.Name.Replace("Buttons", string.Empty);
                var buttonList = CreateButtonsCollection(category);
                prop.SetValue(this, buttonList);
            }
        }


        private List<RecipeSelectButtonVM> CreateButtonsCollection(string cathegory)
        {
            var list = ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory(cathegory)
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in list) button.ObjectSelected += RecipeSelected_EventStarter;

            return list;
        }

        private void RecipeSelected_EventStarter(SatisfactoryRecipe recipe)
        {
            RecipeSelected(recipe);
        }
    }
}
