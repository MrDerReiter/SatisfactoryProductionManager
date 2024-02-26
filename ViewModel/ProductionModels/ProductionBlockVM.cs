﻿using Prism.Mvvm;
using SatisfactoryProductionManager.Model.Production;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SatisfactoryProductionManager.ViewModel.ProductionModels
{
    public class ProductionBlockVM : BindableBase
    {
        private ProductionBlock _sourceBlock;

        public BindingList<ProductionUnitVM> UnitModels { get; }
        public BindingList<RequestButtonVM> RequestButtons { get; }
        public BindingList<ByproductButtonVM> ByproductButtons { get; }
        public EditableRequestButtonVM ProductionRequestButton { get; }


        public ProductionBlockVM(ProductionBlock sourceBlock)
        {
            _sourceBlock = sourceBlock;
            if (sourceBlock == null) return;

            var unitModels = _sourceBlock.ProductionUnits.Select((unit) => new ProductionUnitVM(unit)).ToList();
            UnitModels = new BindingList<ProductionUnitVM>(unitModels);

            RequestButtons = new BindingList<RequestButtonVM>();
            ByproductButtons = new BindingList<ByproductButtonVM>();

            ProductionRequestButton = new EditableRequestButtonVM(_sourceBlock.ProductionRequest);
            ProductionRequestButton.PropertyChanged += UpdateUnitsVM;
        }

        private void UpdateUnitsVM(object sender, PropertyChangedEventArgs args)
        {
            var unitModels = _sourceBlock.ProductionUnits.Select((unit) => new ProductionUnitVM(unit));
            UnitModels.Clear();
            UnitModels.AddRange(unitModels);

            var requestButtons = _sourceBlock.Inputs.Select((input) => new RequestButtonVM(input)).ToList();
            RequestButtons.Clear();
            RequestButtons.AddRange(requestButtons);

            var byproductButtons = _sourceBlock.Byproducts.Select((byproduct) => new ByproductButtonVM(byproduct)).ToList();
            ByproductButtons.Clear();
            ByproductButtons.AddRange(byproductButtons);
        }
    }
}
