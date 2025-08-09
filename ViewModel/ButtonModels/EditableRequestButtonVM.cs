using CustomToolkit.Text;
using FactoryManagementCore;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace SatisfactoryProductionManager;

public class EditableRequestButtonVM : ResourceStreamButtonVM
{
    private readonly Regex _mathExpressionPattern =
        new Regex(@"^\d+\.?\d*\s*[-+*/]\s*\d+\.?\d*$");

    public ResourceStream Request
    {
        get => InnerObject;
        set => InnerObject = value;
    }
    public string RequestValue
    {
        get => Count;
        set
        {
            try
            {
                double newRequestValue;
                CultureInfo invariant = CultureInfo.InvariantCulture;

                newRequestValue = _mathExpressionPattern.IsMatch(value) ?
                        double.Parse(RegexCalculator.Calculate(value), invariant) :
                        double.Parse(value, invariant);

                Request = Request.Variate(newRequestValue);

                RequestValueChanged(newRequestValue);
                OnPropertyChanged(nameof(RequestValue));
            }
            catch
            {
                MessageBox.Show
                    ("Введите корректное неотрицательное целое число, неотрицательное дробное число с плавающей точкой " +
                     "или корректную математическую операцию с двумя соответствующими числами. " +
                     "При использовании операции возвращаемое число также не должно быть отрицательным.",
                     "Некорректное значение запроса",
                      MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }

    public event Action<double> RequestValueChanged;


    public EditableRequestButtonVM(ResourceStream request) : base(request) { }
}
