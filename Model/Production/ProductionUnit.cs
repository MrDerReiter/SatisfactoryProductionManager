using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Collections.Generic;

namespace SatisfactoryProductionManager.Model.Production
{
    public class ProductionUnit
    {
        private readonly ResourceRequest[] _inputs;

        public Recipe Recipe { get; }
        public string Machine { get => Recipe.Machine; }
        public double MachinesCount { get => ProductionRequest.CountPerMinute / Recipe.Product.CountPerMinute; }
        public bool HasByproduct { get => Recipe.HasByproduct; }
        public ResourceRequest ProductionRequest { get; }
        public ResourceStream Byproduct { get; private set; }
        public IReadOnlyList<ResourceRequest> Inputs { get => _inputs; }


        public ProductionUnit(ResourceRequest request, Recipe recipe)
        {
            if (request.Resource != recipe.Product.Resource) throw new InvalidOperationException("Несовпадение выходного ресурса в рецепте и запросе на ресурс");

            Recipe = recipe;
            ProductionRequest = request;
            _inputs = new ResourceRequest[Recipe.Inputs.Length];

            for (int i = 0; i < Recipe.Inputs.Length; i++)
                _inputs[i] = (Recipe.Inputs[i] * MachinesCount).ToRequest();

            if (HasByproduct)
                Byproduct = Recipe.Byproduct.Value * MachinesCount;

            ProductionRequest.RequestChanged += UpdateIO;
        }


        private void UpdateIO()
        {
            for (int i = 0; i < _inputs.Length; i++)
                _inputs[i].CountPerMinute = Recipe.Inputs[i].CountPerMinute * MachinesCount;

            if (HasByproduct)
                Byproduct = Recipe.Byproduct.Value * MachinesCount;
        }
    }
}
