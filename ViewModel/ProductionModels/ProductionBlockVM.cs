using FactoryManagementCore.Production;
using FactoryManagementCore.Elements;
using Prism.Mvvm;
using SatisfactoryProductionManager.Model;
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

        public ProductionBlock SourceBlock { get => _sourceBlock; }
        public BindingList<ProductionUnitVM> UnitModels { get; }
        public BindingList<RequestButtonVM> RequestButtons { get; }
        public BindingList<ByproductButtonVM> ByproductButtons { get; }
        public EditableRequestButtonVM ProductionRequestButton { get; }
        public bool IsSomewhereOverclock
        {
            get
            {
                foreach (var unit in UnitModels)
                    if (unit.IsOverclocked) return true;

                return false;
            }
        }
        public bool IsSomewhereSomersloopUsed
        {
            get
            {
                foreach (var unit in UnitModels)
                    if (unit.IsSomersloopUsed) return true;

                return false;
            }
        }
        public string PowerShardCount
        {
            get
            {
                return UnitModels
                      .Where(unit => unit.IsOverclocked)
                      .Select(unit => int.Parse(unit.PowerShardCount))
                      .Sum().ToString();
            }
        }
        public string SomersloopsCount
        {
            get
            {
                return UnitModels
                      .Where(unit => unit.IsSomersloopUsed)
                      .Select(unit => int.Parse(unit.SomersloopCount))
                      .Sum().ToString();
            }
        }

        public event Action<object> ButtonPressed;
        public event Action<ProductionUnit> RequestingAddBlock;


        public ProductionBlockVM(ProductionBlock sourceBlock)
        {
            _sourceBlock = sourceBlock;
            if (sourceBlock == null) return;

            var unitModels = _sourceBlock.ProductionUnits.Select((unit) => new ProductionUnitVM(unit)).ToList();
            UnitModels = new BindingList<ProductionUnitVM>(unitModels);
            foreach (var unitModel in UnitModels)
                SubscribeToUnitEvents(unitModel);

            var requestButtons = _sourceBlock.Inputs
                .Where(input => input.CountPerMinute > 0)
                .Select((input) => new RequestButtonVM(input)).ToList();
            RequestButtons = new BindingList<RequestButtonVM>(requestButtons);
            foreach (var button in RequestButtons) button.ObjectSelected += RunSelector;

            var byproductButtons = _sourceBlock.Outputs.Skip(1).Select((byproduct) => new ByproductButtonVM(byproduct)).ToList();
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
                MessageBox.Show(ex.Message, "Не удалось удалить цех", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                MessageBox.Show(ex.Message, "Не удалось преобразовать", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ExpandRequestToProductionUnit(ResourceRequest request, Recipe recipe)
        {
            ButtonPressed_EventStarter(null);

            _sourceBlock.AddProductionUnit(request, recipe);
            UpdateUnitsVM(null, null);
        }

        private bool NeedToFullUpdateBlock(object sender, string propertyName)
        {
            if (propertyName == "Overclock" && sender is ProductionUnitVM unitModel)
            {
                if (unitModel.IsSomersloopUsed)
                    RaisePropertyChanged(nameof(SomersloopsCount));

                if (unitModel.IsOverclocked)
                    RaisePropertyChanged(nameof(PowerShardCount));

                return false;
            }

            else if (propertyName == "IsOverclocked")
            {
                RaisePropertyChanged(nameof(IsSomewhereOverclock));
                return false;
            }

            else if (propertyName == "MachineCount" ||
                     propertyName == "PowerShardCount" ||
                     propertyName == "SomersloopCount") return false;

            return true;
        }

        private void UpdateUnitsVM(object sender, PropertyChangedEventArgs args)
        {
            if (!NeedToFullUpdateBlock(sender, args?.PropertyName)) return;

            var unitModels = _sourceBlock.ProductionUnits.Select((unit) => new ProductionUnitVM(unit));
            UnitModels.Clear();
            UnitModels.AddRange(unitModels);
            foreach (var unitModel in UnitModels)
                SubscribeToUnitEvents(unitModel);

            var requestButtons = _sourceBlock.Inputs
                .Where(input => input.CountPerMinute > 0)
                .Select(input => new RequestButtonVM(input));
            RequestButtons.Clear();
            RequestButtons.AddRange(requestButtons);
            foreach (var button in RequestButtons) button.ObjectSelected += RunSelector;

            var byproductButtons = _sourceBlock.Outputs.Skip(1).Select((byproduct) => new ByproductButtonVM(byproduct)).ToList();
            ByproductButtons.Clear();
            ByproductButtons.AddRange(byproductButtons);

            if (args?.PropertyName == "IsSomersloopUsed")
            {
                RaisePropertyChanged(nameof(IsSomewhereSomersloopUsed));
                RaisePropertyChanged(nameof(SomersloopsCount));
                _sourceBlock.RaiseIOChanged();
            }

            if (args?.PropertyName == "RequestValue" && IsSomewhereSomersloopUsed)
                RaisePropertyChanged(nameof(SomersloopsCount));

            RaisePropertyChanged("ProductionBlockIO");
        }

        private void SubscribeToUnitEvents(ProductionUnitVM unitModel)
        {
            unitModel.RequestingRemoveProdUnit += RemoveProdUnit;
            unitModel.RequestingConvertUnitToBlock += ConvertUnitToBlock;
            unitModel.ButtonPressed += ButtonPressed_EventStarter;
            unitModel.PropertyChanged += UpdateUnitsVM;
        }

        private void RunSelector(ResourceRequest request)
        {
            ButtonPressed_EventStarter(null);

            try
            {
                var selector = new RequestRecipeSelector(request);
                var context = selector.DataContext as RequestRecipeSelectorVM;

                if (context.Buttons.Count == 1)
                {
                    ExpandRequestToProductionUnit(request, context.Buttons[0].InnerObject);
                    selector.Close();
                }
                else
                {
                    context.RecipeSelected += ExpandRequestToProductionUnit;
                    selector.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при инициализации выбора рецепта", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
