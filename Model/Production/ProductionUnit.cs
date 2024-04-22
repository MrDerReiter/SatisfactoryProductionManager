using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Collections.Generic;

namespace SatisfactoryProductionManager.Model.Production
{
    public class ProductionUnit
    {
        private double _overclock = 100;
        private readonly ResourceRequest[] _inputs;

        public Recipe Recipe { get; }
        public double Overclock
        {
            get => _overclock;
            set
            {
                if(value > 0 && value <= 250) _overclock = value;
            }
        }
        public string Machine { get => Recipe.Machine; }
        public double MachinesCount { get => ProductionRequest.CountPerMinute / Recipe.Product.CountPerMinute / (Overclock * 0.01); }
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
