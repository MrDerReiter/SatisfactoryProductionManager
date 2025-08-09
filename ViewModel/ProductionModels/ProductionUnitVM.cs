using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomToolkit.Text;
using FactoryManagementCore;
using SatisfactoryProductionManager.Extensions;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SatisfactoryProductionManager;

public partial class ProductionUnitVM : ObservableObject
{
    private static readonly CultureInfo _invariant = CultureInfo.InvariantCulture;
    private static readonly Regex _percentCountPattern = new(@"^\s*\d+\.?\d*%?$");
    private static readonly Regex _mathExpressionPattern = new(@"^\s*\d+\.?\d*\s*[-+*/]\s*\d+\.?\d*$");

    private readonly SatisfactoryProductionUnit _sourceUnit;

    public SatisfactoryProductionUnit SourceUnit => _sourceUnit;
    public ImageSource MachineView { get; }
    public string MachineName { get; }
    public double MachineCount => _sourceUnit.MachineCount;
    public string MachineCountView
    {
        get => MachineCount.ToString("0.###", CultureInfo.InvariantCulture);
        set
        {
            try
            {
                var targetMachineCount = double.Parse(value, CultureInfo.InvariantCulture);
                var targetOverclock = GetOptimalOverclock(targetMachineCount);

                Overclock = targetOverclock;
            }
            catch (FormatException)
            {
                MessageBox.Show
                    ("Неподходящее значение для количества станков. " +
                     "Введите целое число, либо дробное число " +
                     "с точкой в качестве разделителя.",
                     "Введено некорректное значение",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show
                    (ex.Message, "Не удалось установить количество станков",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    public double Overclock
    {
        get => _sourceUnit.Overclock;
        set
        {
            bool wasOvercklockedBefore = IsOverclocked;

            _sourceUnit.Overclock = value;

            OnPropertyChanged(nameof(OverclockView));
            OnPropertyChanged(nameof(MachineCountView));

            if (IsOverclocked != wasOvercklockedBefore)
            {
                OnPropertyChanged(nameof(IsOverclocked));
                OnPropertyChanged(nameof(PowerShardCountView));
                IsOverclockedChanged(IsOverclocked);
            }

            else if (IsOverclocked && wasOvercklockedBefore)
            {
                OnPropertyChanged(nameof(PowerShardCountView));
                PowerShardCountChanged();
            }

            if (IsSomersloopUsed)
            {
                OnPropertyChanged(nameof(SomersloopCountView));
                SomersloopCountChanged();
            }
        }
    }
    public string OverclockView
    {
        get => _sourceUnit.Overclock.ToString("0.#", CultureInfo.InvariantCulture) + "%";
        set
        {
            try
            {
                var result = _mathExpressionPattern.IsMatch(value) ?
                    double.Parse(RegexCalculator.Calculate(value), _invariant) :

                    _percentCountPattern.IsMatch(value) ?
                    double.Parse(value.Replace("%", ""), _invariant) :
                    throw new FormatException();

                Overclock = result;
            }
            catch (Exception)
            {
                MessageBox.Show
                    ("Неподходящее значение для величины разгона. " +
                     "Введите целое число, либо дробное число " +
                     "с точкой в качестве разделителя; " +
                     "допускается опциональный знак % в конце. " +
                     "Также можно ввести корректное бинарное математическое выражение.",
                     "Введено некорректное значение",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public bool IsSomersloopUsed
    {
        get => _sourceUnit.IsSomersloopUsed;
        set
        {
            _sourceUnit.IsSomersloopUsed = value;

            OnPropertyChanged(nameof(IsSomersloopUsed));
            IsSomersloopUsedChanged(value);
        }
    }
    public bool IsOverclocked => _sourceUnit.IsOverclocked;
    public uint PowerShardCount => _sourceUnit.PowerShardCount;
    public uint SomersloopCount => _sourceUnit.SomersloopCount;
    public string PowerShardCountView => PowerShardCount.ToString();
    public string SomersloopCountView => SomersloopCount.ToString();

    [ObservableProperty]
    public partial ResourceStreamButtonVM[] Products { get; private set; }
    [ObservableProperty]
    public partial ResourceStreamButtonVM[] Requests { get; private set; }

    public event Action<ProductionUnit> RemovingProductionUnit;
    public event Action<ProductionUnit> ConvertingUnitToBlock;
    public event Action <bool> IsOverclockedChanged;
    public event Action <bool> IsSomersloopUsedChanged;
    public event Action PowerShardCountChanged;
    public event Action SomersloopCountChanged;
    public event Action ButtonPressed;


    public ProductionUnitVM(ProductionUnit sourceUnit)
    {
        _sourceUnit = (SatisfactoryProductionUnit)sourceUnit;

        string machineImageFilePath = $"../Assets/Machines/{_sourceUnit.Machine}.png";
        MachineView = new BitmapImage(new Uri(machineImageFilePath, UriKind.Relative));
        MachineName = _sourceUnit.Machine.Translate();

        UpdateView();
    }


    private double GetOptimalOverclock(double targetMachineCount)
    {
        double baseMachinesCount =
            _sourceUnit.ProductionRequest.CountPerMinute /
            _sourceUnit.Recipe.Product.CountPerMinute;

        double targetOverclock = baseMachinesCount / targetMachineCount * 100;
        if (IsSomersloopUsed) targetOverclock /= 2;

        if (targetOverclock > 250)
            throw new InvalidOperationException
                ("Невозможно выполнить производственный запрос с заданным количеством станков; " +
                 "даже при максимальном разгоне выход продукции будет недостаточным. " +
                 "В данном цехе станков должно быть как минимум " +
                $"{GetMinMachineCount(baseMachinesCount)} (при максимальном разгоне)");

        return targetOverclock;
    }

    private double GetMinMachineCount(double baseMachinesCount)
    {
        return IsSomersloopUsed ?
            baseMachinesCount / 5 :
            baseMachinesCount / 2.5;
    }

    #region Обработчики команд
    [RelayCommand]
    private void SetSomersloop()
    {
        ButtonPressed();
        IsSomersloopUsed = !IsSomersloopUsed;
    }

    [RelayCommand]
    private void RemoveProductionUnit()
    {
        try
        {
            ButtonPressed();
            RemovingProductionUnit(_sourceUnit);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка при попытке удаления цеха",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void ConvertUnitToBlock()
    {
        try
        {
            ButtonPressed();
            ConvertingUnitToBlock(_sourceUnit);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка при попытке преобразования цеха",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void IncreaseOverclock(double value)
    {
        if (Overclock + value > 250) return;
        Overclock += (double)value;
    }

    [RelayCommand]
    private void DecreaseOverclock(double value)
    {
        if (Overclock - value <= 0) return;
        Overclock -= (double)value;
    }
    #endregion


    public void UpdateView()
    {
        if (_sourceUnit.HasByproduct) Products =
        [
            new ResourceStreamButtonVM(_sourceUnit.ProductionRequest),
            new ResourceStreamButtonVM(_sourceUnit.Byproduct)
        ];
        else Products = [new ResourceStreamButtonVM(_sourceUnit.ProductionRequest)];

        Requests =
        [
            .. _sourceUnit.Inputs
            .Select(input => new ResourceStreamButtonVM(input))
        ];

        OnPropertyChanged(nameof(MachineCountView));
        OnPropertyChanged(nameof(PowerShardCountView));
        OnPropertyChanged(nameof(SomersloopCountView));
    }
}
