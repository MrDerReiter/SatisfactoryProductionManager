using Prism.Commands;
using Prism.Mvvm;
using SatisfactoryProductionManager.Model.Production;
using SatisfactoryProductionManager.Services;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ProductionModels
{
    public class ProductionUnitVM : BindableBase
    {
        private ProductionUnit _sourceUnit;

        public ImageSource Machine { get; }
        public string MachineName { get; }
        public string MachineCount { get => _sourceUnit.MachinesCount.ToString("0.###", CultureInfo.InvariantCulture); }
        public string Overclock
        {
            get => (_sourceUnit.Overclock * 100).ToString("0.###", CultureInfo.InvariantCulture) + "%";
            set
            {
                if (double.TryParse(value.Replace("%", string.Empty), CultureInfo.InvariantCulture, out double result))
                {
                    _sourceUnit.Overclock = result / 100;
                    RaisePropertyChanged(nameof(Overclock));
                    RaisePropertyChanged(nameof(MachineCount));
                }
                    
            }
        }
        public ResourceStreamButtonVM[] Products { get; }
        public RequestButtonVM[] Requests { get; }

        public DelegateCommand RemoveProdUnit { get; }
        public DelegateCommand ConvertUnitToBlock { get; }
        public DelegateCommand IncreaseOverclock { get; }
        public DelegateCommand DecreaseOverclock { get; }

        public event Action<ProductionUnit> RequestingRemoveProdUnit;
        public event Action<ProductionUnit> RequestingConvertUnitToBlock;

        public ProductionUnitVM(ProductionUnit sourceUnit)
        {
            _sourceUnit = sourceUnit;

            Machine = new BitmapImage(new Uri($"../Assets/Machines/{_sourceUnit.Machine}.png", UriKind.Relative));
            MachineName = _sourceUnit.Machine.TranslateRU();

            if (_sourceUnit.HasByproduct)
            {
                Products = new ResourceStreamButtonVM[]
                {
                    new ResourceStreamButtonVM(_sourceUnit.ProductionRequest.ToStream()),
                    new ResourceStreamButtonVM(_sourceUnit.Byproduct)
                };
            }
            else
                Products = new ResourceStreamButtonVM[]
                { new ResourceStreamButtonVM(_sourceUnit.ProductionRequest.ToStream()) };

            Requests = _sourceUnit.Inputs.Select((input) => new RequestButtonVM(input)).ToArray();

            RemoveProdUnit = new DelegateCommand(RemoveProdUnit_CommandHandler);
            ConvertUnitToBlock = new DelegateCommand(ConvertUnitToBlock_CommandHandler);
            IncreaseOverclock = new DelegateCommand(IncreaseOverclock_CommandHandler);
            DecreaseOverclock = new DelegateCommand(DecreaseOverclock_CommandHandler);
        }


        private void RemoveProdUnit_CommandHandler()
        {
            RequestingRemoveProdUnit?.Invoke(_sourceUnit);
        }

        private void ConvertUnitToBlock_CommandHandler()
        {
            RequestingConvertUnitToBlock?.Invoke(_sourceUnit);
        }

        private void IncreaseOverclock_CommandHandler()
        {
            if (_sourceUnit.Overclock > 2) return;

            if (_sourceUnit.Overclock >= 1) _sourceUnit.Overclock += 0.5;
            else _sourceUnit.Overclock += 0.1;

            RaisePropertyChanged(nameof(Overclock));
            RaisePropertyChanged(nameof(MachineCount));
        }

        private void DecreaseOverclock_CommandHandler()
        {
            if (_sourceUnit.Overclock <= 0.1001) return;

            if (_sourceUnit.Overclock >= 1.5) _sourceUnit.Overclock -= 0.5;
            else _sourceUnit.Overclock -= 0.1;

            RaisePropertyChanged(nameof(Overclock));
            RaisePropertyChanged(nameof(MachineCount));
        }
    }
}
