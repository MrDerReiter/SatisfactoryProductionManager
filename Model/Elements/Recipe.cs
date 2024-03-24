using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace SatisfactoryProductionManager.Model.Elements;

/// <summary>
/// Инкапсулирует рецепт, используемый на производственной линии.
/// </summary>
public class Recipe
{
    public string Name { get; }
    public string Category { get; }
    public string Machine { get; }
    public bool HasByproduct { get => Byproduct.HasValue; }
    public ResourceStream Product { get; }
    public ResourceStream? Byproduct { get; }
    public ResourceStream[] Inputs { get; }

    public Recipe
        (string name, string machine, string category,
        ResourceStream product, ResourceStream[] inputs,
        ResourceStream? byproduct = null)
    {
        Name = name;
        Machine = machine;
        Category = category;
        Product = product;
        Byproduct = byproduct;
        Inputs = inputs;
    }
}
