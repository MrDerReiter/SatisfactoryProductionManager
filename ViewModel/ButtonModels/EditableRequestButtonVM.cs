using Prism.Commands;
using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Globalization;
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
                    if (Regex.IsMatch(value, @"^\d*\.?\d*\s*[-+*/]\s*\d*\.?\d*$"))
                    {
                        var leftNumber = double.Parse(Regex.Match(value, @"^\s*\d*\.?\d*").Value, CultureInfo.InvariantCulture);
                        var rightNumber = double.Parse(Regex.Match(value, @"\d*\.?\d*\s*$").Value, CultureInfo.InvariantCulture);
                        var operation = Regex.Match(value, @"[-+*/]").Value;

                        var total = operation switch
                        {
                            "+" => leftNumber + rightNumber,
                            "-" => leftNumber - rightNumber,
                            "*" => leftNumber * rightNumber,
                            "/" => rightNumber != 0 ? leftNumber / rightNumber : throw new InvalidOperationException(),
                            _ => throw new InvalidOperationException()
                        };

                        InnerObject.CountPerMinute = total;
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
                        ("Введите корректное неотрицательное целое число, неотрицательное дробное число с точкой " +
                        "или корректную математическую операцию с двумя соответствующими числами. " +
                        "При использовании операции возвращаемое её число также не должно быть отрицательным.",
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
