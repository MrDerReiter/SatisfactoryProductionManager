using FactoryManagementCore.Elements;

namespace SatisfactoryProductionManager.Model;

public class SatisfactoryRecipe : Recipe
{
    public string Category { get; }
    public bool HasByproduct { get => Byproduct.HasValue; }
    public ResourceStream Product { get; }
    public ResourceStream? Byproduct { get; }

    public SatisfactoryRecipe
        (string name, string machine, string category,
        ResourceStream[] inputs, ResourceStream[] outputs) 
        : base (name, machine, inputs, outputs)
    {
        Category = category;   
        Product = Outputs[0];
        Byproduct = Outputs.Length > 1 ? Outputs[1] : null;
    }
}
