using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Model.Interfaces;
using SatisfactoryProductionManager.Model.Production;
using System;
using System.ComponentModel;
using System.Linq;

namespace SatisfactoryProductionManager.Model
{
    public static class ProductionManager
    {
        private static IFactorySaveLoadManager _SaveLoadManager;
        private static ProductionLine _activeLine;

        public static IRecipeProvider RecipeProvider { get; }
        public static BindingList<ProductionLine> ProductionLines { get; }
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

            ProductionLines = new BindingList<ProductionLine>();
            RecipeProvider = new FileRecipeProvider();
        }

        public static void AddProductionLine(Recipe recipe)
        {
            var line = new ProductionLine(recipe);
            ProductionLines.Add(line);
            ActiveLine = line;
        }

        public static void RemoveActiveLine()
        {
            ProductionLines.Remove(ActiveLine);
            ActiveLine = ProductionLines.FirstOrDefault();
        }

        public static void MoveActiveLineLeft()
        {
            var index = ProductionLines.IndexOf(ActiveLine);

            if (index <= 0) return;

            var temp = ProductionLines[index - 1];
            ProductionLines[index - 1] = ProductionLines[index];
            ProductionLines[index] = temp;
        }

        public static void MoveActiveLineRight()
        {
            var index = ProductionLines.IndexOf(ActiveLine);

            if (index == ProductionLines.Count - 1) return;

            var temp = ProductionLines[index + 1];
            ProductionLines[index + 1] = ProductionLines[index];
            ProductionLines[index] = temp;
        }

        public static void SaveFactory()
        {
            _SaveLoadManager.SaveFactory();
        }
    }
}

