using Prism.Commands;
using Prism.Mvvm;
using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Model.Production;
using SatisfactoryProductionManager.View;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using SatisfactoryProductionManager.ViewModel.ProductionModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SatisfactoryProductionManager.ViewModel
{
    public class MainWindowVM : BindableBase
    {
        private readonly MediaPlayer _player;
        private readonly List<ProductionLineIOVM> _productionLinesIOs;
        private readonly List<ProductionBlockVM> _productionBlockWorkspaces;

        public ProductionLine ActiveLine { get => ProductionManager.ActiveLine; }
        public ProductionBlock ActiveBlock { get; private set; }
        public ProductionLineIOVM ActiveLineIO { get; private set; }
        public ProductionBlockVM ProductionBlockWorkspace { get; private set; }
        public BindingList<ProductionLineButtonVM> ProductionLineButtons { get; }
        public BindingList<ProductionBlockButtonVM> ProductionBlockButtons { get; }


        public DelegateCommand AddProductionLine { get; }
        public DelegateCommand AddProductionBlock { get; }
        public DelegateCommand MoveActiveLineLeft { get; }
        public DelegateCommand MoveActiveLineRight { get; }
        public DelegateCommand RemoveActiveBlock { get; }


        public MainWindowVM()
        {
            _player = new MediaPlayer();
            _player.Open(new Uri("Click.mp3", UriKind.Relative));

            var lines = ProductionManager.ProductionLines.Select(pl => new ProductionLineButtonVM(pl)).ToList();
            ProductionLineButtons = new BindingList<ProductionLineButtonVM>(lines);
            foreach (var button in ProductionLineButtons)
            {
                button.ObjectSelected += PlayPushButtonSound;
                button.ObjectSelected += SetActiveLine;
            }

            _productionLinesIOs = new List<ProductionLineIOVM>();
            foreach (var line in ProductionManager.ProductionLines)
            {
                var lineIO = new ProductionLineIOVM(line);
                _productionLinesIOs.Add(lineIO);
                lineIO.NeedToCreateProductionBlock += AddProductionBlock_CommandHandler;
            }

            _productionBlockWorkspaces = new List<ProductionBlockVM>();
            ProductionManager.ProductionLines
                .SelectMany(line => line.ProductionBlocks)
                .ToList()
                .ForEach(AddProductionBlockVM);


            ProductionBlockButtons = new BindingList<ProductionBlockButtonVM>();

            AddProductionLine = new DelegateCommand(AddProductionLine_CommandHandler);
            MoveActiveLineLeft = new DelegateCommand(MoveActiveLineLeft_CommandHandler);
            MoveActiveLineRight = new DelegateCommand(MoveActiveLineRight_CommandHandler);
            AddProductionBlock = new DelegateCommand(AddProductionBlock_CommandHandler);
            RemoveActiveBlock = new DelegateCommand(RemoveActiveBlock_CommandHandler);

            ProductionManager.ActiveLineChanged += SetProductionBlocks;

            if (ProductionManager.ProductionLines.Count > 0)
                SetActiveLine(ProductionManager.ProductionLines[0]);

        }


        private void AddProductionLine_CommandHandler()
        {
            PlayPushButtonSound();

            var selector = new RecipeSelector();
            var context = selector.DataContext as RecipeSelectorVM;
            context.RecipeSelected += PlayPushButtonSound;
            context.RecipeSelected += CreateProductionLine;
            selector.ShowDialog();
        }

        private void AddProductionBlock_CommandHandler()
        {
            PlayPushButtonSound();
            if (ProductionManager.ActiveLine == null) return;

            var selector = new RecipeSelector();
            var context = selector.DataContext as RecipeSelectorVM;
            context.RecipeSelected += PlayPushButtonSound;
            context.RecipeSelected += CreateProductionBlock;
            selector.ShowDialog();
        }

        private void AddProductionBlock_CommandHandler(ResourceStream stream)
        {
            PlayPushButtonSound();

            try
            {
                var selector = new RequestRecipeSelector(stream.ToRequest());
                var context = selector.DataContext as RequestRecipeSelectorVM;
                context.RecipeSelected += PlayPushButtonSound;
                context.RecipeSelected += CreateProductionBlock;
                selector.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при инициализации выбора рецепта", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MoveActiveLineLeft_CommandHandler()
        {
            PlayPushButtonSound();

            ProductionManager.MoveActiveLineLeft();
            UpdateProductionLineButtons();
        }

        private void MoveActiveLineRight_CommandHandler()
        {
            PlayPushButtonSound();

            ProductionManager.MoveActiveLineRight();
            UpdateProductionLineButtons();
        }

        private void RemoveActiveBlock_CommandHandler()
        {
            PlayPushButtonSound();
            if (ActiveBlock == null) return;

            if (ActiveBlock == ActiveLine.MainProductionBlock)
                RemoveActiveLine();
            else
            {
                ActiveLine.RemoveProductionBlock(ActiveBlock);
                RemoveProductionBlockVM(ActiveBlock);

                SetActiveBlock(ActiveLine.MainProductionBlock);
                SetProductionBlocks(ActiveLine);

                UpdateLineIO();
            }
        }

        private void PlayPushButtonSound()
        {
            _player.Stop();
            _player.Play();
        }

        private void PlayPushButtonSound(object obj)
        {
            PlayPushButtonSound();
        }

        private void PlayPushButtonSound(object obj1, object obj2)
        {
            PlayPushButtonSound();
        }

        private void UpdateProductionLineButtons()
        {
            ProductionLineButtons.Clear();

            var lines = ProductionManager.ProductionLines.Select(pl => new ProductionLineButtonVM(pl)).ToList();
            ProductionLineButtons.AddRange(lines);

            foreach (var button in ProductionLineButtons)
            {
                button.ObjectSelected += PlayPushButtonSound;
                button.ObjectSelected += SetActiveLine;
            }
        }

        private void UpdateLineIO()
        {
            if (ProductionManager.ActiveLine == null) return;

            ActiveLineIO.Update();
        }

        private void UpdateLineIO(object sender, PropertyChangedEventArgs args)
        {
            UpdateLineIO();
        }

        private void CreateProductionLine(Recipe recipe)
        {
            var line = ProductionManager.AddProductionLine(recipe);
            AddProductionBlockVM(line.MainProductionBlock);

            var lineIO = new ProductionLineIOVM(line);
            var lineButton = new ProductionLineButtonVM(line);

            _productionLinesIOs.Add(lineIO);
            lineIO.NeedToCreateProductionBlock += AddProductionBlock_CommandHandler;
            SetActiveLine(line);

            ProductionLineButtons.Add(lineButton);
            lineButton.ObjectSelected += PlayPushButtonSound;
            lineButton.ObjectSelected += SetActiveLine;
        }

        private void CreateProductionBlock(Recipe recipe)
        {
            ProductionManager.ActiveLine.AddProductionBlock(recipe);

            var block = ProductionManager.ActiveLine.ProductionBlocks.Last();
            AddProductionBlockVM(block);

            SetActiveBlock(block);
            SetProductionBlocks(ProductionManager.ActiveLine);
        }

        private void CreateProductionBlock(ResourceRequest request, Recipe recipe)
        {
            ProductionManager.ActiveLine.AddProductionBlock(request, recipe);

            var block = ProductionManager.ActiveLine.ProductionBlocks.Last();
            AddProductionBlockVM(block);

            SetActiveBlock(block);
            SetProductionBlocks(ProductionManager.ActiveLine);

            UpdateLineIO();
        }

        private void CreateProductionBlock(ProductionUnit unit)
        {
            ActiveLine.AddProductionBlock(unit);

            var block = ProductionManager.ActiveLine.ProductionBlocks.Last();
            AddProductionBlockVM(block);

            SetProductionBlocks(ProductionManager.ActiveLine);

            UpdateLineIO();
        }

        private void SetActiveLine(ProductionLine prodLine)
        {
            if (prodLine == null)
            {
                ProductionManager.ActiveLine = null;
                ActiveLineIO = null;
                RaisePropertyChanged(nameof(ActiveLineIO));
                SetActiveBlock(null);
                return;
            }

            ProductionManager.ActiveLine = prodLine;
            ActiveLineIO = _productionLinesIOs
                .First(prodLineIO => prodLineIO.SourceLine == prodLine);
            RaisePropertyChanged(nameof(ActiveLineIO));

            SetActiveBlock(prodLine.MainProductionBlock);
        }

        private void RemoveActiveLine()
        {
            _productionLinesIOs.Remove(ActiveLineIO);

            var activeLineButton = ProductionLineButtons.First((plb) => plb.InnerObject == ActiveLine);
            ProductionLineButtons.Remove(activeLineButton);

            foreach (var block in ActiveLine.ProductionBlocks)
                RemoveProductionBlockVM(block);

            ProductionBlockButtons.Clear();
            ProductionManager.RemoveActiveLine();

            SetActiveLine(ProductionManager.ProductionLines.FirstOrDefault());
        }

        private void SetActiveBlock(ProductionBlock block)
        {
            if (block == null)
            {
                ActiveBlock = null;
                ProductionBlockWorkspace = null;
                RaisePropertyChanged(nameof(ProductionBlockWorkspace));
                return;
            }

            ActiveBlock = block;
            ProductionBlockWorkspace = _productionBlockWorkspaces.First(blockVM => blockVM.SourceBlock == block);
            RaisePropertyChanged(nameof(ProductionBlockWorkspace));
        }

        private void AddProductionBlockVM(ProductionBlock block)
        {
            var blockVM = new ProductionBlockVM(block);

            blockVM.ButtonPressed += PlayPushButtonSound;
            blockVM.RequestingAddBlock += CreateProductionBlock;
            blockVM.PropertyChanged += UpdateLineIO;

            _productionBlockWorkspaces.Add(blockVM);
        }

        private void RemoveProductionBlockVM(ProductionBlock block)
        {
            var deprecatedBlockVM = _productionBlockWorkspaces
                .First(blockVM => blockVM.SourceBlock == block);
            _productionBlockWorkspaces.Remove(deprecatedBlockVM);
        }

        private void SetProductionBlocks(ProductionLine prodLine)
        {
            if (prodLine == null)
            {
                ProductionBlockButtons.Clear();
                return;
            }

            ProductionBlockButtons.Clear();

            foreach (var block in prodLine.ProductionBlocks)
            {
                var button = new ProductionBlockButtonVM(block);
                ProductionBlockButtons.Add(button);
                button.ObjectSelected += PlayPushButtonSound;
                button.ObjectSelected += SetActiveBlock;
            }

        }
    }
}
