using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Globalization;
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
                try
                {
                    InnerObject.CountPerMinute = double.Parse(value, CultureInfo.InvariantCulture);
                    RaisePropertyChanged(nameof(RequestValue));
                }
                catch
                {
                    MessageBox.Show("Некорректное значение запроса");
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
