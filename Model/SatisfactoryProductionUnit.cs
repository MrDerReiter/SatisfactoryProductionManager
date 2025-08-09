using FactoryManagementCore;


namespace SatisfactoryProductionManager;

public class SatisfactoryProductionUnit : ProductionUnit
{
    private bool _isSomersloopUsed;
    private double _overclock = 100;
    private double OverclockModifier => _overclock / 100;
    private double BaseMachineCount {  get; set; }
        

    private static readonly int _somersloopModifier = 2;
    private static readonly Dictionary<string, uint> _somersloopsPerMachine = new()
    {
        { "Constructor", 1 },
        { "Smelter", 1 },
        { "Assembler", 2 },
        { "Refinery", 2 },
        { "Converter", 2 },
        { "Foundry", 2 },
        { "Manufacturer", 4 },
        { "Blender", 4 },
        { "Collider", 4 },
        { "QuantumEncoder", 4 }
    };

    public override SatisfactoryRecipe Recipe { get; }
    public override ResourceStream ProductionRequest 
    { 
        get => base.ProductionRequest;
        set
        {
            BaseMachineCount = value.CountPerMinute /
                               Recipe.Product.CountPerMinute;

            base.ProductionRequest = value;
        }
    }
    public double Overclock
    {
        get => _overclock;
        set
        {
            if (value != _overclock &&
                value > 0 && value <= 250) _overclock = value;
        }
    }
    public bool IsOverclocked => _overclock > 100 && MachineCount >= 1;
    public bool IsSomersloopUsed
    {
        get => _isSomersloopUsed;
        set
        {
            _isSomersloopUsed = value;
            UpdateIO();
        }
    }
    public uint PowerShardCount => GetPowerShardCount();
    public uint SomersloopCount => GetSomersloopCount();
    public bool HasByproduct => Recipe.HasByproducts;
    public ResourceStream Product
    {
        get => _outputs[0];
        private set => _outputs[0] = value;
    }
    public ResourceStream Byproduct
    {
        get => _outputs[1];
        private set => _outputs[1] = value;
    }


    public SatisfactoryProductionUnit
        (ResourceStream productionRequest, SatisfactoryRecipe recipe)
    {
        if (recipe.Product.Resource != productionRequest.Resource)
            throw new InvalidOperationException
                ("Несовпадение выходного ресурса в рецепте и запросе на ресурс");

        Recipe = recipe;
        Machine = recipe.Machine;

        _inputs = new ResourceStream[Recipe.Inputs.Length];
        _outputs = new ResourceStream[Recipe.Outputs.Length];

        ProductionRequest = productionRequest;
    }


    private uint GetPowerShardCount()
    {
        return IsOverclocked ?
               (uint)(Math.Floor(MachineCount) *
               Math.Ceiling((_overclock - 100) / 50)) : 0;
    }

    private uint GetSomersloopCount()
    {
        return _isSomersloopUsed ?
               (uint)Math.Ceiling(MachineCount) * _somersloopsPerMachine[Machine] : 0;
    }

    protected override void UpdateIO()
    {
        for (int i = 0; i < _inputs.Length; i++)
            _inputs[i] = _isSomersloopUsed ?
                Recipe.Inputs[i] * BaseMachineCount / 2 :
                Recipe.Inputs[i] * BaseMachineCount;

        for (int i = 0; i < _outputs.Length; i++)
            _outputs[i] = Recipe.Outputs[i] * BaseMachineCount;
    }

    protected override double GetMachineCount()
    {
        return _isSomersloopUsed ?
               BaseMachineCount / OverclockModifier / _somersloopModifier :
               BaseMachineCount / OverclockModifier;
    }
}
