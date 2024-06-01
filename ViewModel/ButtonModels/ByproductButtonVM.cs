using FactoryManagementCore.Elements;
using System;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class ByproductButtonVM : ObjectButtonVM<ResourceStream>
    {
        public string Count { get => InnerObject.CountPerMinute.ToString("0.###", CultureInfo.InvariantCulture); }

        public ByproductButtonVM(ResourceStream byproduct)
        {
            InnerObject = byproduct;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{byproduct.Resource}.png", UriKind.Relative));
        }
    }
}
