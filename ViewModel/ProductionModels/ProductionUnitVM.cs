using FactoryManagementCore.Production;
using FactoryManagementCore.Services;
using Prism.Commands;
using Prism.Mvvm;
using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ProductionModels
{
    public class ProductionUnitVM : BindableBase
    {
        private SatisfactoryProductionUnit _sourceUnit;

        public ImageSource Machine { get; }
        public string MachineName { get; }
        public string MachineCount { get => _sourceUnit.MachinesCount.ToString("0.###", CultureInfo.InvariantCulture); }
        public string Overclock
        {
            get => _sourceUnit.Overclock.ToString("0.#", CultureInfo.InvariantCulture) + "%";
            set
            {
                try
                {
                    if (Regex.IsMatch(value, @"^\d+\.?\d*\s*[-+*/]\s*\d+\.?\d*$"))
                    {
                         var result = double.Parse(RegexCalculator.Calculate(value), CultureInfo.InvariantCulture);
                        _sourceUnit.Overclock = result;
                    }
                    else if (Regex.IsMatch(value, @"^\d+\.?\d*%?$"))
                    {
                        var result = double.Parse(value.Replace("%", ""), CultureInfo.InvariantCulture);
                        _sourceUnit.Overclock = result;
                    }
                }
                catch (Exception) { }
                finally
                {
                    RaisePropertyChanged(nameof(Overclock));
                    RaisePropertyChanged(nameof(MachineCount));
                }
            }
        }
        public ResourceStreamButtonVM[] Products { get; }
        public RequestButtonVM[] Requests { get; }

        public DelegateCommand RemoveProdUnit { get; }
        public DelegateCommand ConvertUnitToBlock { get; }
        public DelegateCommand<double?> IncreaseOverclock { get; }
        public DelegateCommand<double?> DecreaseOverclock { get; }

        public event Action<ProductionUnit> RequestingRemoveProdUnit;
        public event Action<ProductionUnit> RequestingConvertUnitToBlock;

        public ProductionUnitVM(ProductionUnit sourceUnit)
        {
            _sourceUnit = sourceUnit as SatisfactoryProductionUnit;

            Machine = new BitmapImage(new Uri($"../Assets/Machines/{_sourceUnit.Machine}.png", UriKind.Relative));
            MachineName = _sourceUnit.Machine.TranslateRU();

            if (_sourceUnit.HasByproduct)
            {
                Products =
                [
                    new ResourceStreamButtonVM(_sourceUnit.ProductionRequest.ToStream()),
                    new ResourceStreamButtonVM(_sourceUnit.Byproduct)
                ];
            }
            else
                Products = [new ResourceStreamButtonVM(_sourceUnit.ProductionRequest.ToStream())];

            Requests = _sourceUnit.Inputs.Select((input) => new RequestButtonVM(input)).ToArray();

            RemoveProdUnit = new DelegateCommand(RemoveProdUnit_CommandHandler);
            ConvertUnitToBlock = new DelegateCommand(ConvertUnitToBlock_CommandHandler);
            IncreaseOverclock = new DelegateCommand<double?>(IncreaseOverclock_CommandHandler);
            DecreaseOverclock = new DelegateCommand<double?>(DecreaseOverclock_CommandHandler);
        }


        private void RemoveProdUnit_CommandHandler()
        {
            RequestingRemoveProdUnit?.Invoke(_sourceUnit);
        }

        private void ConvertUnitToBlock_CommandHandler()
        {
            RequestingConvertUnitToBlock?.Invoke(_sourceUnit);
        }

        private void IncreaseOverclock_CommandHandler(double? value)
        {
            if (!value.HasValue) value = 50;
            if (_sourceUnit.Overclock + value > 250) return;

            _sourceUnit.Overclock += (double)value;

            RaisePropertyChanged(nameof(Overclock));
            RaisePropertyChanged(nameof(MachineCount));
        }

        private void DecreaseOverclock_CommandHandler(double? value)
        {
            if (!value.HasValue) value = 50;
            if (_sourceUnit.Overclock - value <= 0) return;

            _sourceUnit.Overclock -= (double)value;

            RaisePropertyChanged(nameof(Overclock));
            RaisePropertyChanged(nameof(MachineCount));
        }
    }
}
