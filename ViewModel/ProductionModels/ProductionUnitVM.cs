using SatisfactoryProductionManager.Model.Production;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ProductionModels
{
    public class ProductionUnitVM 
    {
        public ProductionUnit SourceUnit { get; }
        public decimal Overclock
        {
            get => (decimal)SourceUnit.Overclock * 100;
            set
            {
                SourceUnit.Overclock = (double)value / 100;
            }
        }
        public ImageSource Machine { get; }
        public string MachineCount { get; }
        public ImageSource Product { get; }
        public string ProductCount { get; }
        public ImageSource Buproduct { get; }
        public string BuproductCount { get; }
        public RequestButtonVM[] Requests {  get; }

        public ProductionUnitVM(ProductionUnit sourceUnit)
        {
            SourceUnit = sourceUnit;

            Machine = new BitmapImage(new Uri($"../Assets/Machines/{SourceUnit.Machine}.png"));
            MachineCount = SourceUnit.MachinesCount.ToString();

            Product = new BitmapImage(new Uri($"../Assets/Resources/{SourceUnit.ProductionRequest.Resource}.png"));
            ProductCount = SourceUnit.ProductionRequest.CountPerMinute.ToString();

            Buproduct = new BitmapImage(new Uri($"../Assets/Resources/{SourceUnit.Byproduct.Resource}.png"));
            BuproductCount = SourceUnit.Byproduct.CountPerMinute.ToString();

            Requests = SourceUnit.Inputs.Select((input) => new RequestButtonVM(input)).ToArray();
        }
    }
}
