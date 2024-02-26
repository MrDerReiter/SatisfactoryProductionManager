using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class ByproductButtonVM : ObjectButtonVM<ResourceOverflow>
    {
        public string Count { get => InnerObject.CountPerMinute.ToString(); }

        public ByproductButtonVM(ResourceOverflow byproduct)
        {
            InnerObject = byproduct;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{byproduct.Resource}.png", UriKind.Relative));
        }
    }
}
