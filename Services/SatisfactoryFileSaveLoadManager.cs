using FactoryManagementCore.Production;
using FactoryManagementCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using FactoryManagementCore.Elements;
using SatisfactoryProductionManager.Model;
using System.Linq;

namespace SatisfactoryProductionManager.Services
{
    public class SatisfactoryFileSaveLoadManager : IFactorySaveLoadManager
    {
        private readonly string _filePath = "ProductionLines.cfg";


        private static List<string> SerializeProductionLine(ProductionLine prodLine)
        {
            List<string> serializedLine = ["ProductionLine:", string.Empty];

            foreach (var block in prodLine.ProductionBlocks)
                serializedLine.AddRange(SerializeProductionBlock(block));

            return serializedLine;
        }

        private static List<string> SerializeProductionBlock(ProductionBlock prodBlock)
        {
            List<string> serializedBlock = ["\tProductionBlock:", string.Empty];

            serializedBlock.Add($"\t\tProductionRequest: {prodBlock.ProductionRequest}");
            serializedBlock.Add($"\t\tRecipe: {(prodBlock.MainProductionUnit as SatisfactoryProductionUnit).Recipe.Name}");
            serializedBlock.Add(string.Empty);

            foreach (var unit in prodBlock.ProductionUnits.Cast<SatisfactoryProductionUnit>())
                serializedBlock.AddRange(SerializeProductionUnit(unit));

            return serializedBlock;
        }

        private static List<string> SerializeProductionUnit(SatisfactoryProductionUnit prodUnit)
        {
            List<string> serializedUnit = ["\t\t\tProductionUnit:", string.Empty];

            serializedUnit.Add($"\t\t\t\tProductionRequest: {prodUnit.ProductionRequest}");
            serializedUnit.Add($"\t\t\t\tRecipe: {prodUnit.Recipe.Name}");
            serializedUnit.Add(string.Empty);

            return serializedUnit;
        }

        private static ProductionLine DeserializeProductionLine(string[] savedContent, int index)
        {
            var line = new ProductionLine();

            for (int i = index; i < savedContent.Length; i++)
            {
                if (savedContent[i] == "\tProductionBlock:")
                {
                    var block = DeserializeProductionBlock(savedContent, i + 2);
                    line.AddProductionBlock(block);

                    continue;
                }
                else if (savedContent[i] == "ProductionLine:") break;
                else continue;
            }

            return line;
        }

        private static ProductionBlock DeserializeProductionBlock(string[] savedContent, int index)
        {
            var resource = savedContent[index].Split(' ')[1];
            var countPerMinute = double.Parse(savedContent[index].Split(' ')[2], CultureInfo.InvariantCulture);
            var request = new ResourceRequest(resource, countPerMinute);

            var recipeName = savedContent[index + 1].Split(' ')[1];
            var recipe = (ProductionManager.RecipeProvider as IRecipeProvider<SatisfactoryRecipe>).GetRecipeByName(recipeName);

            var block = new ProductionBlock(new SatisfactoryProductionUnit(request, recipe));
            var savedUnits = new List<SatisfactoryProductionUnit>();

            for (int i = index + 3; i < savedContent.Length; i++)
            {
                if (savedContent[i] == "\t\t\tProductionUnit:")
                {
                    var unit = DeserializeProductionUnit(savedContent, i + 2);
                    savedUnits.Add(unit);
                }
                else if (savedContent[i] == "\tProductionBlock:") break;
                else continue;
            }

            ReconstructProductionBlock(block, savedUnits);

            return block;
        }

        private static SatisfactoryProductionUnit DeserializeProductionUnit(string[] savedContent, int index)
        {
            var resource = savedContent[index].Split(' ')[1];
            var countPerMinute = double.Parse(savedContent[index].Split(' ')[2], CultureInfo.InvariantCulture);
            var request = new ResourceRequest(resource, countPerMinute);

            var recipeName = savedContent[index + 1].Split(' ')[1];
            var recipe = (ProductionManager.RecipeProvider as IRecipeProvider<SatisfactoryRecipe>).GetRecipeByName(recipeName);

            return new SatisfactoryProductionUnit(request, recipe);
        }

        private static void ReconstructProductionBlock(ProductionBlock block, List<SatisfactoryProductionUnit> controlUnitList)
        {
            if (controlUnitList[0].ProductionRequest == block.ProductionRequest)
                controlUnitList.RemoveAt(0);
            else throw new InvalidDataException("Ошибка в сохранённых данных; главный цех производственного блока не совпадает с его сигнатурой.");

            for (int i = 0; i < controlUnitList.Count; i++) 
            {
                for (int j = 0; j < block.Inputs.Count; j++) 
                {
                    if (controlUnitList[i].ProductionRequest == block.Inputs[j]) 
                    {
                        var unit = new SatisfactoryProductionUnit
                            (block.Inputs[j], controlUnitList[i].Recipe);
                        block.AddProductionUnit(unit);
                        controlUnitList.RemoveAt(i);
                        i = -1;
                        break;
                    }
                }
            }
        }


        public List<ProductionLine> LoadFactory()
        {
            try
            {
                string[] savedContent = File.ReadAllLines(_filePath);
                if (savedContent.Length == 0) return new List<ProductionLine>();

                var productionLines = new List<ProductionLine>();
                for (int i = 0; i < savedContent.Length; i++)
                {
                    if (savedContent[i] == "ProductionLine:")
                    {
                        var line = DeserializeProductionLine(savedContent, i + 2);
                        productionLines.Add(line);
                    }
                    else continue;
                }

                return productionLines;
            }
            catch (Exception ex)
            {
                MessageBox.Show
                    ("Не удалось загрузить ранее сохранённую фабрику:\n" + ex.Message,
                    "Ошибка загрузки сохранённой фабрики", MessageBoxButton.OK, MessageBoxImage.Error);

                return new List<ProductionLine>();
            }
        }

        public void SaveFactory()
        {
            var allProdLines = new List<string>();

            try
            {
                foreach (var line in ProductionManager.ProductionLines)
                {
                    var serializedProdLine = SerializeProductionLine(line);
                    allProdLines.AddRange(serializedProdLine);
                    allProdLines.AddRange([string.Empty, string.Empty]);
                }

                File.WriteAllLines(_filePath, allProdLines);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nПри попытке сохранить фабрику возникла ошибка.\nПри следующем сеансе текущая фабрика не будет восстановлена.",
                    "Ошибка при сохранении фабрики", MessageBoxButton.OK, MessageBoxImage.Error);

                File.Create(_filePath);
            }
        }
    }
}
