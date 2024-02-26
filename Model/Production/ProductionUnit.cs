using SatisfactoryProductionManager.Model.Elements;

namespace SatisfactoryProductionManager.Model.Production
{
    public class ProductionUnit
    {
        private double _overclock = 1;

        public Recipe Recipe { get; }
        public string Machine { get => Recipe.Machine; }
        public double MachinesCount { get => ProductionRequest.CountPerMinute / Recipe.Product.CountPerMinute / Overclock; }
        public double Overclock
        {
            get => _overclock;
            set
            {
                if (value > 2.5) _overclock = 2.5;
                else if (value <= 0) _overclock = 0.01;
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
                Inputs[i].CountPerMinute = Recipe.Inputs[i].CountPerMinute * MachinesCount;

            if (HasByproduct)
                Byproduct.CountPerMinute = Recipe.Byproduct.Value.CountPerMinute * MachinesCount;
        }
    }
}
