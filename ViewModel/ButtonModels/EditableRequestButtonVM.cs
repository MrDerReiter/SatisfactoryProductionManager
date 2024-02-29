using Prism.Commands;
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
            get => InnerObject.CountPerMinute.ToString("0.###", CultureInfo.InvariantCulture);
            set
            {
                try
                {
                    InnerObject.CountPerMinute = double.Parse(value, CultureInfo.InvariantCulture);
                    RaisePropertyChanged(nameof(RequestValue));
                }
                catch
                {
                    MessageBox.Show
                        ("Введите корректное целое число или дробное число с точкой",
                        "Некорректное значение запроса",
                        MessageBoxButton.OK, MessageBoxImage.Error);
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
