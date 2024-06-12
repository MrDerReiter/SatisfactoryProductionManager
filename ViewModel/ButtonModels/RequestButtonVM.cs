using FactoryManagementCore.Elements;
using System;
using System.Globalization;
using System.Windows.Media.Imaging;
using SatisfactoryProductionManager.Services;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class RequestButtonVM : ObjectButtonVM<ResourceRequest>
    {
        public string Tooltip { get => InnerObject.Resource.TranslateRU(); }
        public string Count { get => InnerObject.CountPerMinute.ToString("0.###", CultureInfo.InvariantCulture); }


        public RequestButtonVM(ResourceRequest request)
        {
            InnerObject = request;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{request.Resource}.png", UriKind.Relative));
        }
    }
}
