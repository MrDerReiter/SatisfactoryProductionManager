using SatisfactoryProductionManager.Model.Production;
using System;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class ProductionBlockButtonVM : ObjectButtonVM<ProductionBlock>
    {
        public ProductionBlockButtonVM(ProductionBlock prodBlock)
        {
            InnerObject = prodBlock;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{prodBlock.ProductionRequest.Resource}.png", UriKind.Relative));
        }
    }
}
