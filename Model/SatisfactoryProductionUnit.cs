using FactoryManagementCore.Elements;
using FactoryManagementCore.Production;
using System;

namespace SatisfactoryProductionManager.Model
{
    public class SatisfactoryProductionUnit : ProductionUnit
    {
        private double _overclock = 100;
        private double OverclockModifier => _overclock / 100;
        private double SupposedMachinesCount => MachinesCount * OverclockModifier;

        public override SatisfactoryRecipe Recipe { get; }
        public double Overclock
        {
            get => _overclock;
            set
            {
                if (value > 0 && value <= 250) _overclock = value;
            }
        }
        public bool HasByproduct { get => Recipe.HasByproduct; }
        public ResourceStream Byproduct { get; private set; }


        public SatisfactoryProductionUnit(ResourceRequest productionRequest, SatisfactoryRecipe recipe)
        {
            if (recipe.Product.Resource != productionRequest.Resource)
                throw new InvalidOperationException("Несовпадение выходного ресурса в рецепте и запросе на ресурс");

            Recipe = recipe;
            ProductionRequest = productionRequest;
            ProductionRequest.IsSatisfied = true;

            _inputs = new ResourceRequest[Recipe.Inputs.Length];
            for (int i = 0; i < Recipe.Inputs.Length; i++)
                _inputs[i] = (Recipe.Inputs[i] * MachinesCount).ToRequest();

            _outputs = new ResourceStream[Recipe.Outputs.Length];
            for (int i = 0; i < Recipe.Outputs.Length; i++)
                _outputs[i] = Recipe.Outputs[i] * MachinesCount;

            if (HasByproduct)
                Byproduct = Recipe.Byproduct.Value * MachinesCount;

            ProductionRequest.RequestChanged += UpdateIO;
        }


        protected override void UpdateIO()
        {
            for (int i = 0; i < _inputs.Length; i++)
                _inputs[i].CountPerMinute = Recipe.Inputs[i].CountPerMinute * SupposedMachinesCount;

            for (int i = 0; i < _outputs.Length; i++)
                _outputs[i] = Recipe.Outputs[i] * SupposedMachinesCount;

            if (HasByproduct)
                Byproduct = Recipe.Byproduct.Value * SupposedMachinesCount;
        }

        protected override double GetMachinesCount()
        {
            return ProductionRequest.CountPerMinute /
                   Recipe.Product.CountPerMinute /
                   (_overclock / 100);
        }
    }
}
