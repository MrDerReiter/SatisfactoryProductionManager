using FactoryManagementCore;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager;

public class ProductionLineButtonVM : ObjectButtonVM<ProductionLine>
{
    public ProductionLineButtonVM(ProductionLine prodLine)
    {
        string resource = prodLine.MainProductionBlock.ProductionRequest.Resource;
        string imageFilePath = $"../Assets/Resources/{resource}.png";

        InnerObject = prodLine;
        Image = new BitmapImage(new Uri(imageFilePath, UriKind.Relative));
    }


    protected override void SelectObject()
    {
        try { base.SelectObject(); }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка при выборе производственной линии",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
