using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Model.Interfaces;
using SatisfactoryProductionManager.Model.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity;

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
                if (_productionLines.Contains(value) || value == null) _activeLine = value;
                else throw new InvalidOperationException
                        ("Попытка использовать в качестве активной линию, которой нет в списке менеджера.");
            }
        }

        static ProductionManager()
        {
            _SaveLoadManager = CommonUnityDIContainer.GetContainer().Resolve<IFactorySaveLoadManager>();
            RecipeProvider = CommonUnityDIContainer.GetContainer().Resolve<IRecipeProvider>();

            var savedData = _SaveLoadManager.LoadFactory();
            _productionLines = new BindingList<ProductionLine>(savedData);
        }


        public static ProductionLine AddProductionLine(Recipe recipe)
        {
            var line = new ProductionLine(recipe);
            _productionLines.Add(line);
            return line;
        }

        public static void RemoveActiveLine()
        {
            _productionLines.Remove(ActiveLine);
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

