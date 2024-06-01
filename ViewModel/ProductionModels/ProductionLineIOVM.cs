using FactoryManagementCore.Production;
using FactoryManagementCore.Elements;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.ComponentModel;

namespace SatisfactoryProductionManager.ViewModel.ProductionModels
{
    public class ProductionLineIOVM
    {
        private ProductionLine _sourceLine;

        public ProductionLine SourceLine { get => _sourceLine; }
        public BindingList<ResourceStreamButtonVM> InputButtons { get; } = new();
        public BindingList<ResourceStreamButtonVM> OutputButtons { get; } = new();

        public event Action<ResourceStream> NeedToCreateProductionBlock;


        public ProductionLineIOVM(ProductionLine sourceLine)
        {
            _sourceLine = sourceLine;
            Update();
        }


        private void NeedToCreateProductionBlock_EventStarter(ResourceStream stream)
        {
            NeedToCreateProductionBlock?.Invoke(stream);
        }


        public void Update()
        {
            Clear();

            foreach (var input in _sourceLine.Inputs)
            {
                var button = new ResourceStreamButtonVM(input);
                InputButtons.Add(button);
                button.ObjectSelected += NeedToCreateProductionBlock_EventStarter;
            }

            foreach (var output in _sourceLine.Outputs)
                OutputButtons.Add(new ResourceStreamButtonVM(output));
        }

        public void Clear()
        {
            InputButtons.Clear();
            OutputButtons.Clear();
        }
    }
}
