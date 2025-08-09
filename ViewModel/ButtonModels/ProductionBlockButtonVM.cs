using FactoryManagementCore;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager;

public class ProductionBlockButtonVM : ObjectButtonVM<ProductionBlock>
{
    public ProductionBlockButtonVM(ProductionBlock prodBlock)
    {
        string imageFilePath = $"../Assets/Resources/{prodBlock.ProductionRequest.Resource}.png";

        InnerObject = prodBlock;
        Image = new BitmapImage(new Uri(imageFilePath, UriKind.Relative));
    }


    protected override void SelectObject()
    {
        try { base.SelectObject(); }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка при выборе производственного блока",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
