﻿using FactoryManagementCore.Interfaces;
using FactoryManagementCore.Production;
using SatisfactoryProductionManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SatisfactoryProductionManager.Model
{
    public static class ProductionManager
    {
        private static IFactorySaveLoadManager _saveLoadManager;
        private static ProductionLine _activeLine;
        private static readonly BindingList<ProductionLine> _productionLines;

        public static IReadOnlyList<ProductionLine> ProductionLines { get => _productionLines; }
        public static IRecipeProvider<SatisfactoryRecipe> RecipeProvider { get; }
        public static INameTranslator NameTranslator { get; }
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
            _saveLoadManager = new SaveLoadManagerStub();
            RecipeProvider = new SatisfactoryFileRecipeProvider();
            NameTranslator = new FileNameTranslatorRU();

            var savedData = _saveLoadManager.LoadFactory();
            _productionLines = new BindingList<ProductionLine>(savedData);
        }


        public static ProductionLine AddProductionLine(SatisfactoryRecipe recipe)
        {
            var line = new ProductionLine();
            line.AddProductionBlock(recipe);
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
            _saveLoadManager.SaveFactory();
        }
    }
}

