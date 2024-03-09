using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryProductionManager.ViewModel
{
    public class RecipeSelectorVM
    {
        public List<RecipeSelectButtonVM> IngotsButtons { get; }
        public List<RecipeSelectButtonVM> MineralsButtons { get; }
        public List<RecipeSelectButtonVM> StandartPartsButtons { get; }
        public List<RecipeSelectButtonVM> IndustrialPartsButtons { get; }
        public List<RecipeSelectButtonVM> ElectronicsButtons { get; }
        public List<RecipeSelectButtonVM> CommunicationsButtons { get; }
        public List<RecipeSelectButtonVM> SpaceElevatorPartsButtons { get; }
        public List<RecipeSelectButtonVM> SuppliesButtons { get; }
        public List<RecipeSelectButtonVM> LiquidsButtons { get; }
        public List<RecipeSelectButtonVM> PackagesButtons { get; }
        public List<RecipeSelectButtonVM> BurnableButtons { get; }
        public List<RecipeSelectButtonVM> NuclearButtons { get; }

        public event Action<Recipe> RecipeSelected;


        public RecipeSelectorVM()
        {
            IngotsButtons = CreateButtonsCollection("Ingots");
            MineralsButtons = CreateButtonsCollection("Minerals");
            StandartPartsButtons = CreateButtonsCollection("StandartParts");
            IndustrialPartsButtons = CreateButtonsCollection("IndustrialParts");
            ElectronicsButtons = CreateButtonsCollection("Electronics");
            CommunicationsButtons = CreateButtonsCollection("Communications");
            SpaceElevatorPartsButtons = CreateButtonsCollection("SpaceElevatorParts");
            SuppliesButtons = CreateButtonsCollection("Supplies");
            LiquidsButtons = CreateButtonsCollection("Liquids");
            PackagesButtons = CreateButtonsCollection("Packages");
            BurnableButtons = CreateButtonsCollection("Burnable");
            NuclearButtons = CreateButtonsCollection("Nuclear");
        }


        private List<RecipeSelectButtonVM> CreateButtonsCollection(string cathegory)
        {
            var list = ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory(cathegory)
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in list) button.ObjectSelected += RecipeSelected_EventStarter;

            return list;
        }

        private void RecipeSelected_EventStarter(Recipe recipe)
        {
            RecipeSelected(recipe);
        }
    }
}
