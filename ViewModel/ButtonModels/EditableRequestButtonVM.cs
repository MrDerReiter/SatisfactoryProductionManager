using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public class EditableRequestButtonVM : ObjectButtonVM<ResourceRequest>
    {
        public string RequestValue
        {
            get => InnerObject.CountPerMinute.ToString();
            set
            {
                if (double.TryParse(value, out double result))
                {
                    InnerObject.CountPerMinute = result;
                    RaisePropertyChanged(nameof(RequestValue));
                }  
            }
        }


        public EditableRequestButtonVM(ResourceRequest request)
        {
            InnerObject = request;
            ImageSource = new BitmapImage(new Uri($"../Assets/Resources/{request.Resource}.png", UriKind.Relative));
        }
    }
}
