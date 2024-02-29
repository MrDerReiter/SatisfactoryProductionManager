using Prism.Mvvm;
using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Model.Production;
using SatisfactoryProductionManager.View;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SatisfactoryProductionManager.ViewModel.ProductionModels
{
    public class ProductionBlockVM : BindableBase
    {
        private ProductionBlock _sourceBlock;

        public BindingList<ProductionUnitVM> UnitModels { get; }
        public BindingList<RequestButtonVM> RequestButtons { get; }
        public BindingList<ByproductButtonVM> ByproductButtons { get; }
        public EditableRequestButtonVM ProductionRequestButton { get; }

        public event Action<object> ButtonPressed;
        public event Action<ProductionUnit> RequestingAddBlock;


        public ProductionBlockVM(ProductionBlock sourceBlock)
        {
            _sourceBlock = sourceBlock;
            if (sourceBlock == null) return;

            var unitModels = _sourceBlock.ProductionUnits.Select((unit) => new ProductionUnitVM(unit)).ToList();
            UnitModels = new BindingList<ProductionUnitVM>(unitModels);
            foreach ( var unitModel in UnitModels )
            {
                unitModel.RequestingRemoveProdUnit += ButtonPressed_EventStarter;
                unitModel.RequestingRemoveProdUnit += RemoveProdUnit;
                unitModel.RequestingConvertUnitToBlock += ButtonPressed_EventStarter;
                unitModel.RequestingConvertUnitToBlock += ConvertUnitToBlock;
            }
            
            var requestButtons = _sourceBlock.Inputs.Select((input) => new RequestButtonVM(input)).ToList();
            RequestButtons = new BindingList<RequestButtonVM>(requestButtons);
            foreach (var button in RequestButtons) button.ObjectSelected += RunSelector;

            var byproductButtons = _sourceBlock.Byproducts.Select((byproduct) => new ByproductButtonVM(byproduct)).ToList();
            ByproductButtons = new BindingList<ByproductButtonVM>(byproductButtons);

            ProductionRequestButton = new EditableRequestButtonVM(_sourceBlock.ProductionRequest);
            ProductionRequestButton.ObjectSelected += ButtonPressed_EventStarter;
            ProductionRequestButton.PropertyChanged += UpdateUnitsVM;
        }


        private void ButtonPressed_EventStarter(object obj)
        {
            ButtonPressed?.Invoke(null);
        }

        private void RemoveProdUnit(ProductionUnit unit)
        {
            try
            {
                _sourceBlock.RemoveProductionUnit(unit);
                UpdateUnitsVM(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Не удалось удалить цех", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ConvertUnitToBlock(ProductionUnit unit)
        {
            try
            {
                _sourceBlock.RemoveProductionUnit(unit);
                RequestingAddBlock?.Invoke(unit);
                UpdateUnitsVM(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не удалось преобразовать цех", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void UpdateUnitsVM(object sender, PropertyChangedEventArgs args)
        {
            var unitModels = _sourceBlock.ProductionUnits.Select((unit) => new ProductionUnitVM(unit));
            UnitModels.Clear();
            UnitModels.AddRange(unitModels);
            foreach (var unitModel in UnitModels)
            {
                unitModel.RequestingRemoveProdUnit += ButtonPressed_EventStarter;
                unitModel.RequestingRemoveProdUnit += RemoveProdUnit;
                unitModel.RequestingConvertUnitToBlock += ButtonPressed_EventStarter;
                unitModel.RequestingConvertUnitToBlock += ConvertUnitToBlock;
            }

            var requestButtons = _sourceBlock.Inputs.Select((input) => new RequestButtonVM(input)).ToList();
            RequestButtons.Clear();
            RequestButtons.AddRange(requestButtons);
            foreach (var button in RequestButtons) button.ObjectSelected += RunSelector;

            var byproductButtons = _sourceBlock.Byproducts.Select((byproduct) => new ByproductButtonVM(byproduct)).ToList();
            ByproductButtons.Clear();
            ByproductButtons.AddRange(byproductButtons);
        }

        private void RunSelector(ResourceRequest request)
        {
            ButtonPressed_EventStarter(null);

            try
            {
                var selector = new RequestRecipeSelector(request);
                var context = selector.DataContext as RequestRecipeSelectorVM;
                context.RecipeSelected += ExpandRequestToProductionUnit;

                selector.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при инициализации выбора рецепта", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExpandRequestToProductionUnit(ResourceRequest request, Recipe recipe)
        {
            ButtonPressed_EventStarter(null);

            _sourceBlock.AddProductionUnit(request, recipe);
            UpdateUnitsVM(null, null);
        }
    }
}
