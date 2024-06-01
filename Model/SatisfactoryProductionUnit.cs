using FactoryManagementCore.Elements;
using FactoryManagementCore.Production;

namespace SatisfactoryProductionManager.Model
{
    public class SatisfactoryProductionUnit : ProductionUnit
    {
        private double _overclock = 100;

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


        public SatisfactoryProductionUnit (ResourceRequest request, SatisfactoryRecipe recipe) : base(request, recipe)
        {
            Recipe = recipe;

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
            base.UpdateIO();

            if (HasByproduct)
                Byproduct = Recipe.Byproduct.Value * MachinesCount;
        }

        protected override double GetMachinesCount()
        {
            return
                ProductionRequest.CountPerMinute /
                Recipe.Outputs[0].CountPerMinute /
                (_overclock / 100);
        }
    }
}
