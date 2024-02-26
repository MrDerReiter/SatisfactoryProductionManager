using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
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
        public List<RecipeSelectButtonVM> LiquidsButtons { get; }

        public event Action<Recipe> RecipeSelected;


        public RecipeSelectorVM()
        {
            IngotsButtons =
                ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory("Ingots")
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in IngotsButtons) button.ObjectSelected += RecipeSelected_EventStarter;

            MineralsButtons =
                ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory("Minerals")
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in MineralsButtons) button.ObjectSelected += RecipeSelected_EventStarter;

            StandartPartsButtons =
                ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory("StandartParts")
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in StandartPartsButtons) button.ObjectSelected += RecipeSelected_EventStarter;

            IndustrialPartsButtons =
                ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory("IndustrialParts")
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in IndustrialPartsButtons) button.ObjectSelected += RecipeSelected_EventStarter;

            ElectronicsButtons =
                ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory("Electronics")
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in ElectronicsButtons) button.ObjectSelected += RecipeSelected_EventStarter;

            CommunicationsButtons =
                ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory("Communications")
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in CommunicationsButtons) button.ObjectSelected += RecipeSelected_EventStarter;

            SpaceElevatorPartsButtons =
                ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory("SpaceElevatorParts")
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in SpaceElevatorPartsButtons) button.ObjectSelected += RecipeSelected_EventStarter;

            LiquidsButtons =
                ProductionManager.RecipeProvider
                .GetAllRecipiesOfCategory("Liquids")
                .Select(rc => new RecipeSelectButtonVM(rc)).ToList();
            foreach (var button in LiquidsButtons) button.ObjectSelected += RecipeSelected_EventStarter;
        }


        private void RecipeSelected_EventStarter(Recipe recipe)
        {
            RecipeSelected(recipe);
        }
    }
}
