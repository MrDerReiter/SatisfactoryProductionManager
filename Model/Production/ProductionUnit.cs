using SatisfactoryProductionManager.Model.Elements;
using System;

namespace SatisfactoryProductionManager.Model.Production
{
    public class ProductionUnit
    {
        private decimal _overclock = 1m;

        public Recipe Recipe { get; }
        public string Machine { get => Recipe.Machine; }
        public decimal MachinesCount { get => ProductionRequest.CountPerMinute / Recipe.Product.CountPerMinute / Overclock; }
        public decimal Overclock
        {
            get => _overclock;
            set
            {
                if (value > 2.5m) _overclock = 2.5m;
                else if (value <= 0) _overclock = 0.01m;
                else _overclock = value;
            }
        }
        public bool HasByproduct { get => Recipe.HasByproduct; }
        public ResourceRequest ProductionRequest { get; }
        public ResourceOverflow Byproduct { get; }
        public ResourceRequest[] Inputs { get; }


        public ProductionUnit(ResourceRequest request, Recipe recipe)
        {
            Recipe = recipe;
            ProductionRequest = request;
            Inputs = new ResourceRequest[Recipe.Inputs.Length];

            for (int i = 0; i < Recipe.Inputs.Length; i++)
                Inputs[i] = (Recipe.Inputs[i] * MachinesCount).ToRequest();

            if (HasByproduct)
                Byproduct = (Recipe.Byproduct.Value * MachinesCount).ToOverflow();

            ProductionRequest.RequestChanged += UpdateIO;
        }


        private void UpdateIO()
        {
            for (int i = 0; i < Inputs.Length; i++)
            {
                Inputs[i].CountPerMinute = Recipe.Inputs[i].CountPerMinute * MachinesCount;
                if (Math.Round(Inputs[i].CountPerMinute - Inputs[i].CountPerMinute) < 0.01m )
                    Inputs[i].CountPerMinute = Math.Round(Inputs[i].CountPerMinute);
            }
                

            if (HasByproduct)
            {
                Byproduct.CountPerMinute = Recipe.Byproduct.Value.CountPerMinute * MachinesCount;
                if (Math.Round(Byproduct.CountPerMinute - Byproduct.CountPerMinute) < 0.01m)
                    Byproduct.CountPerMinute = Math.Round(Byproduct.CountPerMinute);
            }
        }
    }
}
