using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FactoryManagementCore;
using FactoryManagementCore.Extensions;
using System.Windows;
using System.Windows.Media;


namespace SatisfactoryProductionManager;

public partial class MainWindowVM : ObservableObject
{
    private readonly MediaPlayer _player;
    private readonly List<ProductionLine> _productionLines;
    private readonly Dictionary<ProductionLine, ProductionLineIOVM> _productionLinesIOs = [];
    private readonly Dictionary<ProductionBlock, ProductionBlockVM> _productionBlockWorkspaces = [];

    private ProductionLine _activeLine;
    private ProductionBlock _activeBlock;

    public ProductionLine ActiveLine
    {
        get => _activeLine;
        private set
        {
            if (value is null)
            {
                _activeLine = null;
                ActiveLineIO = null;
                ActiveBlock = null;
                IsLineSelected = false;

                UpdateProductionBlockButtons();
                return;
            }

            _activeLine = value;
            ActiveLineIO = _productionLinesIOs[value];
            IsLineSelected = true;

            UpdateProductionBlockButtons();
            ActiveBlock = _activeLine.MainProductionBlock;
        }
    }
    public ProductionBlock ActiveBlock
    {
        get => _activeBlock;
        private set
        {
            if (value is null)
            {
                _activeBlock = null;

                ProductionBlockWorkspace = null;
                IsBlockSelected = false;
                return;
            }

            _activeBlock = value;
            ProductionBlockWorkspace = _productionBlockWorkspaces[value];
            IsBlockSelected = true;
        }
    }

    [ObservableProperty]
    public partial bool IsLineSelected { get; private set; }
    [ObservableProperty]
    public partial bool IsBlockSelected { get; private set; }
    [ObservableProperty]
    public partial ProductionLineIOVM ActiveLineIO { get; private set; }
    [ObservableProperty]
    public partial ProductionBlockVM ProductionBlockWorkspace { get; private set; }
    [ObservableProperty]
    public partial List<ProductionLineButtonVM> ProductionLineButtons { get; private set; } = [];
    [ObservableProperty]
    public partial List<ProductionBlockButtonVM> ProductionBlockButtons { get; private set; } = [];


    public MainWindowVM()
    {
        _player = new MediaPlayer();
        _player.Open(new Uri("Click.mp3", UriKind.Relative));

        _productionLines = App.FactoryKeeper.LoadFactory();
        if (_productionLines.Count == 0) return;

        _productionLines.ForEach(AddViewModel);
        _productionLines.SelectMany(line => line.ProductionBlocks)
                        .ForEach(AddViewModel);

        ActiveLine = _productionLines[0];
    }


    private void PlayPushButtonSound()
    {
        _player.Stop();
        _player.Play();
    }

    private void AddProductionLine(Recipe recipe)
    {
        var unit = App.GetUnitInstance(recipe);
        var block = new ProductionBlock(unit);
        var line = new ProductionLine(block);

        _productionLines.Add(line);

        AddViewModel(line);
        AddViewModel(block);

        ActiveLine = line;

        UpdateProductionLineButtons();
    }

    private void AddProductionBlock<T>(T parameter)
    {
        #region Локальные функции
        static ProductionBlock GetBlockFromRecipe(Recipe recipe)
        {
            var unit = App.GetUnitInstance(recipe);
            return new ProductionBlock(unit);
        }

        static ProductionBlock GetBlockFromUnit(ProductionUnit unit)
        {
            return new ProductionBlock(unit);
        }

        static ProductionBlock GetBlockFromPair(ResourceStream request, Recipe recipe)
        {
            var unit = App.GetUnitInstance(request, recipe);
            return new ProductionBlock(unit);
        }

        void CallSelector(ResourceStream request)
        {
            var selector = new RequestRecipeSelector(request);
            var context = selector.DataContext as RequestRecipeSelectorVM;

            if (context.RecipeButtons.Count == 1)
            {
                AddProductionBlock
                    (ValueTuple.Create(request, context.RecipeButtons[0].InnerObject));

                selector.Close();
                return;
            }

            context.RecipeSelected += (request, recipe) =>
            {
                PlayPushButtonSound();
                AddProductionBlock(ValueTuple.Create(request, recipe));
            };
            selector.ShowDialog();
        }
        #endregion

        if (!IsLineSelected) return;
        ProductionBlock block = null;

        switch (parameter)
        {
            case Recipe givenRecipe:
                block = GetBlockFromRecipe(givenRecipe); break;

            case ProductionUnit givenUnit:
                block = GetBlockFromUnit(givenUnit); break;

            case ValueTuple<ResourceStream, Recipe> givenTuple:
                var (request, recipe) = givenTuple;
                block = GetBlockFromPair(request, recipe); break;

            case ResourceStream givenRequest:
                CallSelector(givenRequest); return;

            default:
                throw new ArgumentException
                    ($"Недопустимый тип аргумента {parameter.GetType().Name}",
                      nameof(parameter));
        }

        ActiveLine.AddProductionBlock(block);
        AddViewModel(block);
        ActiveBlock = block;

        UpdateProductionBlockButtons();
    }

    private void AddViewModel<T>(T parameter)
    {
        switch (parameter)
        {
            case ProductionLine line:

                var lineIO = new ProductionLineIOVM(line);
                var lineButton = new ProductionLineButtonVM(line);

                _productionLinesIOs[line] = lineIO;
                lineIO.ResourceStreamToBlock += (stream) =>
                {
                    PlayPushButtonSound();
                    AddProductionBlock(stream);
                };

                ProductionLineButtons.Add(lineButton);
                lineButton.ObjectSelected += (line) =>
                {
                    PlayPushButtonSound();
                    ActiveLine = line;
                }; break;


            case ProductionBlock block:

                var blockVM = new ProductionBlockVM(block);

                blockVM.ButtonPressed += PlayPushButtonSound;
                blockVM.ProductionUnitToBlock += AddProductionBlock;

                _productionBlockWorkspaces[block] = blockVM; break;


            default:
                throw new ArgumentException
                    ($"Недопустимый тип аргумента {parameter.GetType().Name}",
                      nameof(parameter));
        }
    }

    private void UpdateProductionLineButtons()
    {
        ProductionLineButtons =
        [
            .. _productionLines
            .Select(line => new ProductionLineButtonVM(line))
        ];
        ProductionLineButtons.ForEach(button =>
        {
            button.ObjectSelected += (line) =>
            {
                PlayPushButtonSound();
                ActiveLine = line;
            };
        });
    }

    private void UpdateProductionBlockButtons()
    {
        if (!IsLineSelected)
        {
            ProductionBlockButtons = [];
            return;
        }

        ProductionBlockButtons =
        [
            .. ActiveLine.ProductionBlocks
            .Select(block => new ProductionBlockButtonVM(block))
        ];
        ProductionBlockButtons.ForEach(button =>
        {
            button.ObjectSelected += (block) =>
            {
                PlayPushButtonSound();
                ActiveBlock = block;
            };
        });
    }

    #region Обработчики команд
    [RelayCommand]
    private void AddProductionLine()
    {
        PlayPushButtonSound();

        try
        {
            var selector = new RecipeSelector();
            var context = selector.DataContext as RecipeSelectorVM;
            context.RecipeSelected += (recipe) =>
            {
                PlayPushButtonSound();
                AddProductionLine(recipe);
            };

            selector.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка при инициализации производственной линии",
                            MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void AddProductionBlock()
    {
        PlayPushButtonSound();
        if (!IsLineSelected) return;

        try
        {
            var selector = new RecipeSelector();
            var context = selector.DataContext as RecipeSelectorVM;

            context.RecipeSelected += (recipe) =>
            {
                PlayPushButtonSound();
                AddProductionBlock(recipe);
            };
            selector.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show
                (ex.Message, "Ошибка при инициализации производственного блока",
                 MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void MoveActiveLineLeft()
    {
        PlayPushButtonSound();
        if (!IsLineSelected) return;

        int index = _productionLines.IndexOf(ActiveLine);
        if (index == 0) return;

        (_productionLines[index - 1], _productionLines[index]) =
            (_productionLines[index], _productionLines[index - 1]);

        UpdateProductionLineButtons();
    }

    [RelayCommand]
    private void MoveActiveLineRight()
    {
        PlayPushButtonSound();
        if (ActiveLine is null) return;

        int index = _productionLines.IndexOf(ActiveLine);
        if (index == _productionLines.Count - 1) return;

        (_productionLines[index], _productionLines[index + 1]) =
            (_productionLines[index + 1], _productionLines[index]);

        UpdateProductionLineButtons();
    }

    [RelayCommand]
    private void RemoveActiveBlock()
    {
        void RemoveActiveLine()
        {
            _productionLines.Remove(ActiveLine);
            _productionLinesIOs.Remove(ActiveLine);

            ActiveLine.ProductionBlocks.ForEach
                (block => _productionBlockWorkspaces.Remove(block));

            ActiveLine = _productionLines.FirstOrDefault();

            UpdateProductionLineButtons();
            UpdateProductionBlockButtons();
        }


        PlayPushButtonSound();
        if (!IsBlockSelected) return;

        if (ActiveBlock == ActiveLine.MainProductionBlock) RemoveActiveLine();
        else
        {
            ActiveLine.RemoveProductionBlock(ActiveBlock);
            _productionBlockWorkspaces.Remove(ActiveBlock);

            ActiveBlock = ActiveLine.MainProductionBlock;

            UpdateProductionBlockButtons();
        }
    }

    [RelayCommand]
    private void SaveFactory()
    {
        App.FactoryKeeper.SaveFactory(_productionLines);
    }
    #endregion
}
