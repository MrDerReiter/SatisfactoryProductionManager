using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.Model.Elements;
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

        public event Action<ResourceRequest,Recipe> RecipeSelected;


        public RequestRecipeSelectorVM(ResourceRequest request)
        {
            _request = request;

            Buttons = 
                ProductionManager.RecipeProvider
                .GetAllRecipiesOfProduct(_request.Resource)
                .Select((recipe) => new RecipeSelectButtonVM(recipe)).ToList();
            foreach (var button in Buttons) button.ObjectSelected += RecipeSelected_EventStarter;
        }


        private void RecipeSelected_EventStarter(Recipe recipe)
        {
            RecipeSelected(_request, recipe);
        }
    }
}
