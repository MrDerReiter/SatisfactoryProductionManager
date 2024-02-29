using Prism.Commands;
using Prism.Mvvm;
using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Model.Production;
using SatisfactoryProductionManager.View;
using SatisfactoryProductionManager.ViewModel.ButtonModels;
using SatisfactoryProductionManager.ViewModel.ProductionModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace SatisfactoryProductionManager.ViewModel
{
    public class MainWindowVM : BindableBase
    {
        private MediaPlayer Player { get; }

        public ProductionBlock ActiveBlock { get; private set; }
        public ProductionBlockVM ProductionBlockWorkspace {  get; private set; }
        public BindingList<ProductionLineButtonVM> ProductionLineButtons { get; }
        public BindingList<ProductionBlockButtonVM> ProductionBlockButtons { get; }


        public DelegateCommand AddProductionLine { get; }
        public DelegateCommand AddProductionBlock { get; }
        public DelegateCommand MoveActiveLineLeft { get; }
        public DelegateCommand MoveActiveLineRight { get; }
        public DelegateCommand RemoveActiveBlock { get; }


        public MainWindowVM()
        {
            Player = new MediaPlayer();
            Player.Open(new Uri("Click.mp3", UriKind.Relative));

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
            PlayPushButtonSound(null);

            var selector = new RecipeSelector();
            var context = selector.DataContext as RecipeSelectorVM;
            context.RecipeSelected += PlayPushButtonSound;
            context.RecipeSelected += CreateProductionLine;
            selector.ShowDialog();
        }

        private void AddProductionBlock_CommandHandler()
        {
            PlayPushButtonSound(null);
            if (ProductionManager.ActiveLine == null) return;

            var selector = new RecipeSelector();
            var context = selector.DataContext as RecipeSelectorVM;
            context.RecipeSelected += PlayPushButtonSound;
            context.RecipeSelected += CreateProductionBlock;
            selector.ShowDialog();
        }

        private void MoveActiveLineLeft_CommandHandler()
        {
            PlayPushButtonSound(null);

            ProductionManager.MoveActiveLineLeft();
            UpdateProductionLineButtons();
        }

        private void MoveActiveLineRight_CommandHandler()
        {
            PlayPushButtonSound(null);

            ProductionManager.MoveActiveLineRight();
            UpdateProductionLineButtons();
        }

        private void RemoveActiveBlock_CommandHandler()
        {
            PlayPushButtonSound(null);
            if (ActiveBlock == null) return;

            if (ActiveBlock == ProductionManager.ActiveLine.MainProductionBlock)
            {
                var activeLineButton = ProductionLineButtons.First((plb) => plb.InnerObject == ProductionManager.ActiveLine);
                ProductionManager.RemoveActiveLine();
                ProductionLineButtons.Remove(activeLineButton);
                SetActiveBlock(ActiveBlock = ProductionManager.ActiveLine?.MainProductionBlock ?? null);
            }
            else
            {
                ProductionManager.ActiveLine.RemoveProductionBlock(ActiveBlock);

                SetActiveBlock(ProductionManager.ActiveLine.MainProductionBlock);
                SetProductionBlocks(ProductionManager.ActiveLine);
            }
        }

        private void PlayPushButtonSound(object obj)
        {
            Player.Stop();
            Player.Play();
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
            SetActiveBlock(line.MainProductionBlock);

            var button = new ProductionLineButtonVM(line);
            ProductionLineButtons.Add(button);
            button.ObjectSelected += PlayPushButtonSound;
            button.ObjectSelected += SetActiveLine;
        }

        private void CreateProductionBlock(Recipe recipe)
        {
            ProductionManager.ActiveLine.AddProductionBlock(recipe);
            SetActiveBlock(ProductionManager.ActiveLine.ProductionBlocks.Last());
            SetProductionBlocks(ProductionManager.ActiveLine);
        }

        private void CreateProductionBlock(ProductionUnit unit)
        {
            ProductionManager.ActiveLine.AddProductionBlock(unit);
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
            ProductionBlockWorkspace = new ProductionBlockVM(block);
            ProductionBlockWorkspace.ButtonPressed += PlayPushButtonSound;
            ProductionBlockWorkspace.RequestingAddBlock += CreateProductionBlock;
            RaisePropertyChanged(nameof(ProductionBlockWorkspace));
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
