using FactoryManagementCore.Elements;
using System.Collections.Generic;

namespace SatisfactoryProductionManager.Model;

public class SatisfactoryRecipe : Recipe
{
    public string Category { get; }
    public bool HasByproduct => Byproduct is not null;
    public ResourceStream Product => Outputs[0];
    public ResourceStream Byproduct => Outputs?.Count > 1 ? Outputs[1] : null;


    public SatisfactoryRecipe
        (string name, string machine, string category,
        List<ResourceStream> inputs, List<ResourceStream> outputs)
            : base(name, machine, inputs, outputs)
    {
        Category = category;
    }

    public SatisfactoryRecipe
        (string name, string machine, string category) : base(name, machine)
    {
        Category = category;
    }
}
