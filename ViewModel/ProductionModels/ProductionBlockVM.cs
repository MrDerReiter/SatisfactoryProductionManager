using Prism.Mvvm;
using SatisfactoryProductionManager.Model.Production;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System.ComponentModel;
using System.Linq;

namespace SatisfactoryProductionManager.ViewModel.ProductionModels
{
    public class ProductionBlockVM : BindableBase
    {
        private ProductionBlock _sourceBlock;

        public ProductionBlock SourceBlock
        {
            get => _sourceBlock;
            set
            {
                if (value != _sourceBlock)
                {
                    _sourceBlock = value;
                    RaisePropertyChanged(nameof(SourceBlock));
                }
            }
        }
        public BindingList<ProductionUnitVM> UnitModels { get; }
        public BindingList<RequestButtonVM> RequestButtons { get; }
        public BindingList<ByproductButtonVM> ByproductButtons { get; }
        public EditableRequestButtonVM ProductionRequestButton { get; }


        public ProductionBlockVM(ProductionBlock sourceBlock)
        {
            _sourceBlock = sourceBlock;

            var unitModels = SourceBlock.ProductionUnits.Select((unit) => new ProductionUnitVM(unit)).ToList();
            UnitModels = new BindingList<ProductionUnitVM>(unitModels);

            var requestButtons = SourceBlock.Inputs.Select((input) => new RequestButtonVM(input)).ToList();
            RequestButtons = new BindingList<RequestButtonVM>(requestButtons);

            var byproductButtons = SourceBlock.Byproducts.Select((byproduct) => new ByproductButtonVM(byproduct)).ToList();
            ByproductButtons = new BindingList<ByproductButtonVM>(byproductButtons);

            ProductionRequestButton = new EditableRequestButtonVM(SourceBlock.ProductionRequest);
        }
    }
}
