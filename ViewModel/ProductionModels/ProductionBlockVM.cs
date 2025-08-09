using CommunityToolkit.Mvvm.ComponentModel;
using FactoryManagementCore;
using FactoryManagementCore.Extensions;
using System.Collections.ObjectModel;


namespace SatisfactoryProductionManager;

public partial class ProductionBlockVM : ObservableObject
{
    private readonly ProductionBlock _sourceBlock;
    private uint PowerShardCount => GetPowerShardCount();
    private uint SomersloopCount => GetSomersloopCount();

    public ProductionBlock SourceBlock => _sourceBlock;
    public EditableRequestButtonVM ProductionRequestButton { get; }
    public string PowerShardCountView => PowerShardCount.ToString();
    public string SomersloopCountView => SomersloopCount.ToString();

    [ObservableProperty]
    public partial bool IsSomewhereOverclocked { get; private set; }
    [ObservableProperty]
    public partial bool IsSomewhereSomersloopUsed { get; private set; }
    [ObservableProperty]
    public partial ObservableCollection<ProductionUnitVM> UnitModels { get; private set; }
    [ObservableProperty]
    public partial List<ResourceStreamButtonVM> RequestButtons { get; private set; }
    [ObservableProperty]
    public partial List<ResourceStreamButtonVM> ByproductButtons { get; private set; }

    public event Action ButtonPressed;
    public event Action<ProductionUnit> ProductionUnitToBlock;


    public ProductionBlockVM(ProductionBlock sourceBlock)
    {
        _sourceBlock = sourceBlock;

        UpdateViewModels(true);

        if (PowerShardCount > 0) IsSomewhereOverclocked = true;
        if (SomersloopCount > 0) IsSomewhereSomersloopUsed = true;

        ProductionRequestButton = new EditableRequestButtonVM(_sourceBlock.ProductionRequest);
        ProductionRequestButton.ObjectSelected += request => ButtonPressed();
        ProductionRequestButton.RequestValueChanged += (value) =>
        {
            var newRequest = _sourceBlock.ProductionRequest.Variate(value);
            _sourceBlock.ProductionRequest = newRequest;

            UpdateViewModels(false);
            UnitModels.ForEach(unit => unit.UpdateView());
        };
    }


    private uint GetPowerShardCount()
    {
        return (uint)UnitModels
            .Where(model => model.IsOverclocked)
            .Select(model => (int)model.PowerShardCount)
            .Sum();
    }

    private uint GetSomersloopCount()
    {
        return (uint)UnitModels
            .Where(model => model.IsSomersloopUsed)
            .Select(model => (int)model.SomersloopCount)
            .Sum();
    }

    private void CallRecipeSelector(ResourceStream request)
    {
        static bool OnlyOneStandartRecipeAvaliable(List<RecipeSelectButtonVM> buttons)
        {
            var recipies = buttons.Select(button => button.InnerObject).ToArray();

            return recipies.Length == 1 &&
                   recipies[0].Category != "Converting";
        }


        var selector = new RequestRecipeSelector(request);
        var context = selector.DataContext as RequestRecipeSelectorVM;

        if (OnlyOneStandartRecipeAvaliable(context.RecipeButtons))
        {
            var defaultRecipe = context.RecipeButtons[0].InnerObject;

            AddProductionUnit(request, defaultRecipe);
            selector.Close();
        }
        else
        {
            context.RecipeSelected += (request, recipe) =>
            {
                ButtonPressed();
                AddProductionUnit(request, recipe);
            };
            selector.ShowDialog();
        }
    }


    private void AddProductionUnit(ResourceStream request, Recipe recipe)
    {
        var unit = App.GetUnitInstance(request, recipe);
        _sourceBlock.AddProductionUnit(unit);

        var unitVM = new ProductionUnitVM(unit);
        SubscribeToUnitModelEvents(unitVM);
        UnitModels.Add(unitVM);

        UpdateViewModels(false);
    }

    private void RemoveProductionUnit(ProductionUnit unit)
    {
        _sourceBlock.RemoveProductionUnit(unit);
        UpdateViewModels(true);
    }

    private void SubscribeToUnitModelEvents(ProductionUnitVM unit)
    {
        unit.ButtonPressed += () => ButtonPressed();
        unit.RemovingProductionUnit += RemoveProductionUnit;
        unit.ConvertingUnitToBlock += (unit) =>
        {
            RemoveProductionUnit(unit);
            ProductionUnitToBlock(unit);
        };

        unit.IsOverclockedChanged += (overclocked) =>
        {
            if (overclocked || PowerShardCount > 0)
            {
                IsSomewhereOverclocked = true;
                OnPropertyChanged(nameof(PowerShardCountView));
            }
            else IsSomewhereOverclocked = false;
        };
        unit.IsSomersloopUsedChanged += (somersloopUsed) =>
        {
            _sourceBlock.UpdateIO();
            UpdateViewModels(false);
            UnitModels.ForEach(unit => unit.UpdateView());

            if (somersloopUsed || SomersloopCount > 0)
            {
                IsSomewhereSomersloopUsed = true;
                OnPropertyChanged(nameof(SomersloopCountView));
            }
            else IsSomewhereSomersloopUsed = false;
        };

        unit.PowerShardCountChanged += () => OnPropertyChanged(nameof(PowerShardCountView));
        unit.SomersloopCountChanged += () => OnPropertyChanged(nameof(SomersloopCountView));
    }

    private void UpdateViewModels(bool includeUnitModels)
    {
        RequestButtons =
        [
            .. _sourceBlock.Inputs
            .Select(input => new ResourceStreamButtonVM(input))
        ];
        RequestButtons.ForEach((button) =>
        {
            button.ObjectSelected += (stream) =>
            {
                ButtonPressed();
                CallRecipeSelector(stream);
            };
        });

        ByproductButtons =
        [
            .. _sourceBlock.Outputs.Skip(1)
            .Select((byproduct) => new ResourceStreamButtonVM(byproduct))
        ];

        if (includeUnitModels)
        {
            UnitModels =
            [
            .. _sourceBlock.ProductionUnits
                .Select(unit => new ProductionUnitVM(unit))
            ];
            UnitModels.ForEach(SubscribeToUnitModelEvents);

            IsSomewhereOverclocked = PowerShardCount > 0;
            if (IsSomewhereOverclocked) OnPropertyChanged(nameof(PowerShardCountView));

            IsSomewhereSomersloopUsed = SomersloopCount > 0;
            if (IsSomewhereSomersloopUsed) OnPropertyChanged(nameof(SomersloopCountView));
        }
    }
}
