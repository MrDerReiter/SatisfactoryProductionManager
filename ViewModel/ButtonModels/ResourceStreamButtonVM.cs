using FactoryManagementCore;
using SatisfactoryProductionManager.Extensions;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager;

public partial class ResourceStreamButtonVM : ObjectButtonVM<ResourceStream>
{
    public string Tooltip => InnerObject.Resource.Translate();
    public string Count => InnerObject.CountPerMinute
        .ToString("0.###", CultureInfo.InvariantCulture);


    public ResourceStreamButtonVM(ResourceStream stream)
    {
        string imageFilePath = $"../Assets/Resources/{stream.Resource}.png";

        InnerObject = stream;
        Image = new BitmapImage(new Uri(imageFilePath, UriKind.Relative));
    }


    protected override void SelectObject()
    {
        try { base.SelectObject(); }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка при обработке запроса",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
