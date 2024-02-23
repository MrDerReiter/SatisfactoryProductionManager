using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class ResourceStreamButtonVM : ObjectButtonVM<ResourceStream>
    {
        public string Count { get => InnerObject.CountPerMinute.ToString(); }


        public ResourceStreamButtonVM(ResourceStream stream)
        {
            InnerObject = stream;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{stream.Resource}.png", UriKind.Relative));
        }
    }
}
