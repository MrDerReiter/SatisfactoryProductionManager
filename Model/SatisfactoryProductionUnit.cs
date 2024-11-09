using FactoryManagementCore.Elements;
using FactoryManagementCore.Production;
using System;
using System.Collections.Generic;

namespace SatisfactoryProductionManager.Model
{
    public class SatisfactoryProductionUnit : ProductionUnit
    {
        private bool _isSomersloopUsed;
        private double _overclock = 100;
        private double OverclockModifier => _overclock / 100;
        private double BaseMachinesCount => ProductionRequest.CountPerMinute / Recipe.Product.CountPerMinute;

        private static readonly int _somersloopModifier = 2;
        private static readonly Dictionary<string, int> _howManySomersloopsNeed = new()
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
        public double Overclock
        {
            get => _overclock;
            set
            {
                if (value != _overclock &&
                    value > 0 && value <= 250)
                {
                    var wasOverclockedBefore = IsOverclocked;

                    _overclock = value;
                    OverclockChanged?.Invoke();

                    if ((!wasOverclockedBefore && IsOverclocked) ||
                       (wasOverclockedBefore && !IsOverclocked)) OverclockedStatusChanged?.Invoke();
                }
            }
        }
        public int PowerShardCount => GetPowerShardCount();
        public int SomersloopCount => GetSomersloopCount();
        public bool IsOverclocked => _overclock > 100 && MachinesCount >= 1;
        public bool IsSomersloopUsed
        {
            get => _isSomersloopUsed;
            set
            {
                _isSomersloopUsed = value;
                UpdateIO();
            }
        }
        public bool HasByproduct { get => Recipe.HasByproduct; }
        public ResourceStream Product
        {
            get => _outputs[0];
            private set => _outputs[0] = value;
        }
        public ResourceStream Byproduct
        {
            get => HasByproduct ? _outputs[1] :
                throw new InvalidOperationException
                    ("Невозможно запросить данные о побочном продукте, " +
                     "т.к. используемый в производственном цехе рецепт не подразумевает его наличие.");

            private set
            {
                if (HasByproduct) _outputs[1] = value;
                else throw new InvalidOperationException
                    ("Невозможно задать значение для побочного продукта, " +
                     "т.к. используемый в производственном цехе рецепт не подразумевает его наличие.");
            }
        }

        public event Action OverclockChanged;
        public event Action OverclockedStatusChanged;


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

            Product = Recipe.Outputs[0] * MachinesCount;

            if (HasByproduct) Byproduct = Recipe.Byproduct.Value * MachinesCount;

            ProductionRequest.RequestChanged += UpdateIO;
        }


        private int GetPowerShardCount()
        {
            if (!IsOverclocked) return 0;

            return (int)(Math.Floor(MachinesCount) *
                         Math.Ceiling((_overclock - 100) / 50));
        }

        private int GetSomersloopCount()
        {
            if (!_isSomersloopUsed) return 0;
            else return (int)Math.Ceiling(MachinesCount) * _howManySomersloopsNeed[Machine];
        }

        protected override void UpdateIO()
        {
            for (int i = 0; i < _inputs.Length; i++)
                _inputs[i].CountPerMinute = _isSomersloopUsed ?
                    Recipe.Inputs[i].CountPerMinute * BaseMachinesCount / _somersloopModifier :
                    Recipe.Inputs[i].CountPerMinute * BaseMachinesCount;

            Product = Recipe.Product * BaseMachinesCount;

            if (HasByproduct)
                Byproduct = Recipe.Byproduct.Value * BaseMachinesCount;
        }

        protected override double GetMachinesCount()
        {
            if (_isSomersloopUsed)
                return BaseMachinesCount / OverclockModifier / _somersloopModifier;

            else return BaseMachinesCount / OverclockModifier;
        }


        public SatisfactoryProductionUnit CloneWithNewRequestInstance(ResourceRequest newRequestInstance)
        {
            if (newRequestInstance.Resource != ProductionRequest.Resource)
                throw new ArgumentNullException
                    (nameof(newRequestInstance),
                    "Новый экземпляр производственного запроса для " +
                    "клонированного цеха должен соответствовать сигнатуре исходного запроса.");

            var cloneUnit = MemberwiseClone() as SatisfactoryProductionUnit;
            cloneUnit.Dispose();

            cloneUnit.ProductionRequest = newRequestInstance;
            cloneUnit.ProductionRequest.RequestChanged += cloneUnit.UpdateIO;
            cloneUnit.UpdateIO();

            return cloneUnit;
        }
    }
}
