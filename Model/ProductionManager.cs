using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Model.Interfaces;
using SatisfactoryProductionManager.Model.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SatisfactoryProductionManager.Model
{
    public static class ProductionManager
    {
        private static IFactorySaveLoadManager _SaveLoadManager;
        private static ProductionLine _activeLine;
        private static readonly BindingList<ProductionLine> _productionLines;

        public static IReadOnlyList<ProductionLine> ProductionLines { get => _productionLines; }
        public static IRecipeProvider RecipeProvider { get; }
        public static ProductionLine LastLine { get => _productionLines.Last(); }
        public static ProductionLine ActiveLine
        {
            get => _activeLine;
            set
            {
                _activeLine = value;
                ActiveLineChanged?.Invoke(ActiveLine);
            }
        }

        public static event Action<ProductionLine> ActiveLineChanged;

        static ProductionManager()
        {
            _SaveLoadManager = new FileSaveLoadManager();
            RecipeProvider = new FileRecipeProvider();

            var savedData = _SaveLoadManager.LoadFactory();
            _productionLines = new BindingList<ProductionLine>(savedData);
        }

        public static void AddProductionLine(ProductionLine prodLine)
        {
            _productionLines.Add(prodLine);
        }

        public static void AddProductionLine(Recipe recipe)
        {
            var line = new ProductionLine(recipe);
            _productionLines.Add(line);
            ActiveLine = line;
        }

        public static void RemoveActiveLine()
        {
            _productionLines.Remove(ActiveLine);
            ActiveLine = ProductionLines.FirstOrDefault();
        }

        public static void MoveActiveLineLeft()
        {
            var index = _productionLines.IndexOf(ActiveLine);

            if (index <= 0) return;

            var temp = _productionLines[index - 1];
            _productionLines[index - 1] = _productionLines[index];
            _productionLines[index] = temp;
        }

        public static void MoveActiveLineRight()
        {
            var index = _productionLines.IndexOf(ActiveLine);

            if (index == _productionLines.Count - 1) return;

            var temp = _productionLines[index + 1];
            _productionLines[index + 1] = _productionLines[index];
            _productionLines[index] = temp;
        }

        public static void SaveFactory()
        {
            _SaveLoadManager.SaveFactory();
        }
    }
}

