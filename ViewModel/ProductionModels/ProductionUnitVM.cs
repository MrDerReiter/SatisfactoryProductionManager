using SatisfactoryProductionManager.Model.Production;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ProductionModels
{
    public class ProductionUnitVM
    {
        private ProductionUnit _sourceUnit { get; }

        public decimal Overclock
        {
            get => _sourceUnit.Overclock * 100;
            set
            {
                _sourceUnit.Overclock = value / 100;
            }
        }
        public ImageSource Machine { get; }
        public string MachineCount { get; }
        public ResourceStreamButtonVM[] Products { get; }
        public RequestButtonVM[] Requests { get; }

        public ProductionUnitVM(ProductionUnit sourceUnit)
        {
            _sourceUnit = sourceUnit;

            Machine = new BitmapImage(new Uri($"../Assets/Machines/{_sourceUnit.Machine}.png", UriKind.Relative));
            MachineCount = _sourceUnit.MachinesCount.ToString("0.###", CultureInfo.InvariantCulture);

            if (_sourceUnit.HasByproduct)
            {
                Products = new ResourceStreamButtonVM[]
                {
                    new ResourceStreamButtonVM(_sourceUnit.ProductionRequest.ToStream()),
                    new ResourceStreamButtonVM(_sourceUnit.Byproduct.ToStream())
                };
            }
            else
                Products = new ResourceStreamButtonVM[]
                { new ResourceStreamButtonVM(_sourceUnit.ProductionRequest.ToStream()) };

            Requests = _sourceUnit.Inputs.Select((input) => new RequestButtonVM(input)).ToArray();
        }
    }
}
