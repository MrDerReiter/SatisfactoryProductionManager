using SatisfactoryProductionManager.Model.Production;
using System;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class ProductionLineButtonVM : ObjectButtonVM<ProductionLine>
    {
        public ProductionLineButtonVM(ProductionLine prodLine)
        {
            InnerObject = prodLine;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{prodLine.MainProductionBlock.ProductionRequest.Resource}.png", UriKind.Relative));
        }
    }
}
