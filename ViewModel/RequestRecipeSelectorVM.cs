using FactoryManagementCore;


namespace SatisfactoryProductionManager;

public class RequestRecipeSelectorVM
{
    public List<RecipeSelectButtonVM> RecipeButtons { get; }

    public event Action<ResourceStream, Recipe> RecipeSelected;


    public RequestRecipeSelectorVM(ResourceStream request)
    {
        RecipeButtons =
        [
            .. App.RecipeProvider
            .GetSubset(recipe => recipe.Product.HasSameResource(request))
            .Select(recipe => new RecipeSelectButtonVM(recipe))
        ];

        RecipeButtons.ForEach(button =>
        {
            button.ObjectSelected +=
                recipe => RecipeSelected(request, recipe);
        });
    }
}
