using FactoryManagementCore.Elements;
using FactoryManagementCore.Interfaces;
using FactoryManagementCore.Production;
using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryProductionManager.ViewModel
{
    public class RequestRecipeSelectorVM
    {
        private ResourceRequest _request;

        public List<RecipeSelectButtonVM> Buttons { get; }

        public event Action<ResourceRequest, SatisfactoryRecipe> RecipeSelected;


        public RequestRecipeSelectorVM(ResourceRequest request)
        {
            _request = request;

            Buttons =
                (ProductionManager.RecipeProvider as IRecipeProvider<SatisfactoryRecipe>)
                .GetAllRecipiesOfProduct(_request.Resource)
                .Select((recipe) => new RecipeSelectButtonVM(recipe)).ToList();
            foreach (var button in Buttons) button.ObjectSelected += RecipeSelected_EventStarter;
        }
    

        private void RecipeSelected_EventStarter(SatisfactoryRecipe recipe)
        {
            RecipeSelected(_request, recipe);
        }
    }
}
