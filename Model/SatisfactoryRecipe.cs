using FactoryManagementCore;


namespace SatisfactoryProductionManager;

public class SatisfactoryRecipe : Recipe
{
    public string Machine => AllowedMachines[0];
    public ResourceStream Product => Outputs[0];
    public ResourceStream Byproduct => HasByproducts ? 
        Outputs[1] : throw new InvalidOperationException
            ("У данного рецепта не может быть побочного продукта.");
}
