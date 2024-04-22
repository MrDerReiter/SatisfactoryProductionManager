using Prism.Commands;
using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Services;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
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
                    if(Regex.IsMatch(value, @"^\d+\.?\d*\s*[-+*/]\s*\d+\.?\d*$"))
                    {
                        var result = double.Parse(RegexCalculator.Calculate(value), CultureInfo.InvariantCulture);

                        InnerObject.CountPerMinute = result;
                        RaisePropertyChanged(nameof(RequestValue));
                    }
                    else
                    {
                        InnerObject.CountPerMinute = double.Parse(value, CultureInfo.InvariantCulture);
                        RaisePropertyChanged(nameof(RequestValue));
                    }
                }
                catch
                {
                    MessageBox.Show
                        ("Введите корректное неотрицательное целое число, неотрицательное дробное число с плавающей точкой " +
                        "или корректную математическую операцию с двумя соответствующими числами. " +
                        "При использовании операции возвращаемое число также не должно быть отрицательным.",
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
