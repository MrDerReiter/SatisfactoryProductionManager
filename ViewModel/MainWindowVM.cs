using Prism.Commands;
using Prism.Mvvm;
using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Model.Production;
using SatisfactoryProductionManager.View;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SatisfactoryProductionManager.ViewModel
{
    public class MainWindowVM : BindableBase
    {
        private ProductionBlock _activeBlock;

        public ProductionBlock ActiveBlock
        {
            get => _activeBlock;
            private set
            {
                if (_activeBlock != value)
                {
                    _activeBlock = value;
                    RaisePropertyChanged(nameof(ActiveBlock));
                }
            }
        }
        public BindingList<ProductionLineButtonVM> ProductionLineButtons { get; }
        public BindingList<ProductionBlockButtonVM> ProductionBlockButtons { get; }
        
        public DelegateCommand AddProductionLine { get; }
        public DelegateCommand AddProductionBlock { get; }
        public DelegateCommand MoveActiveLineLeft { get; }
        public DelegateCommand MoveActiveLineRight { get; }
        public DelegateCommand RemoveActiveBlock { get; }


        public MainWindowVM()
        {
            var lines = ProductionManager.ProductionLines.Select(pl => new ProductionLineButtonVM(pl)).ToList();
            ProductionLineButtons = new BindingList<ProductionLineButtonVM>(lines);
            foreach (var line in ProductionLineButtons) line.ObjectSelected += SetActiveLine;

            ProductionBlockButtons = new BindingList<ProductionBlockButtonVM>();

            AddProductionLine = new DelegateCommand(AddProductionLine_CommandHandler);
            MoveActiveLineLeft= new DelegateCommand(MoveActiveLineLeft_CommandHandler);
            MoveActiveLineRight = new DelegateCommand(MoveActiveLineRight_CommandHandler);
            AddProductionBlock = new DelegateCommand(AddProductionBlock_CommandHandler);
            RemoveActiveBlock = new DelegateCommand(RemoveActiveBlock_CommandHandler);

            ProductionManager.ActiveLineChanged += SetProductionBlocks;
        }

        

        private void AddProductionLine_CommandHandler()
        {
            var selector = new RecipeSelector();
            var context = selector.DataContext as RecipeSelectorVM;
            context.RecipeSelected += CreateProductionLine;
            selector.ShowDialog();
        }

        private void AddProductionBlock_CommandHandler()
        {
            if (ProductionManager.ActiveLine == null) return;

            var selector = new RecipeSelector();
            var context = selector.DataContext as RecipeSelectorVM;
            context.RecipeSelected += CreateProductionBlock;
            selector.ShowDialog();
        }

        private void MoveActiveLineLeft_CommandHandler()
        {
            ProductionManager.MoveActiveLineLeft();
            UpdateProductionLineButtons();
        }

        private void MoveActiveLineRight_CommandHandler()
        {
            ProductionManager.MoveActiveLineRight();
            UpdateProductionLineButtons();
        }

        private void RemoveActiveBlock_CommandHandler()
        {
            if (ActiveBlock == null) return;

            if (ActiveBlock == ProductionManager.ActiveLine.MainProductionBlock)
            {
                var activeLineButton = ProductionLineButtons.First((plb) => plb.InnerObject == ProductionManager.ActiveLine);
                ProductionManager.RemoveActiveLine();
                ProductionLineButtons.Remove(activeLineButton);
                ActiveBlock = ProductionManager.ActiveLine?.MainProductionBlock ?? null;
            }
            else
            {
                ProductionManager.ActiveLine.RemoveProductionBlock(ActiveBlock);

                ActiveBlock = ProductionManager.ActiveLine.MainProductionBlock;
                SetProductionBlocks(ProductionManager.ActiveLine);
            }
        }

        private void UpdateProductionLineButtons()
        {
            ProductionLineButtons.Clear();

            var lines = ProductionManager.ProductionLines.Select(pl => new ProductionLineButtonVM(pl)).ToList();
            ProductionLineButtons.AddRange(lines);
            foreach (var line in ProductionLineButtons) line.ObjectSelected += SetActiveLine;
        }

        private void CreateProductionLine(Recipe recipe)
        {
            ProductionManager.AddProductionLine(recipe);

            var line = ProductionManager.ProductionLines.Last();
            ActiveBlock = line.MainProductionBlock;

            var button = new ProductionLineButtonVM(line);
            ProductionLineButtons.Add(button);
            button.ObjectSelected += SetActiveLine;
        }

        private void CreateProductionBlock(Recipe recipe)
        {
            ProductionManager.ActiveLine.AddProductionBlock(recipe);
            ActiveBlock = ProductionManager.ActiveLine.ProductionBlocks.Last();
            SetProductionBlocks(ProductionManager.ActiveLine);
        }

        private void SetActiveLine(ProductionLine prodLine)
        {
            ProductionManager.ActiveLine = prodLine;
            SetActiveBlock(prodLine.MainProductionBlock);
        }

        private void SetActiveBlock(ProductionBlock block)
        {
            ActiveBlock = block;
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
                button.ObjectSelected += SetActiveBlock;
            }
                
        }
    }
}
