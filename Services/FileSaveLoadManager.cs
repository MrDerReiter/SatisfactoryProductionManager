using SatisfactoryProductionManager.Model;
using SatisfactoryProductionManager.Model.Elements;
using SatisfactoryProductionManager.Model.Interfaces;
using SatisfactoryProductionManager.Model.Production;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;

namespace SatisfactoryProductionManager.Services
{
    public class FileSaveLoadManager : IFactorySaveLoadManager
    {
        private readonly string _filePath = $"{Environment.CurrentDirectory}\\ProductionLines.txt";


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
            serializedBlock.Add($"\t\tRecipe: {prodBlock.MainProductionUnit.Recipe.Name}");
            serializedBlock.Add(string.Empty);

            foreach (var unit in prodBlock.ProductionUnits)
                serializedBlock.AddRange(SerializeProductionUnit(unit));

            return serializedBlock;
        }

        private static List<string> SerializeProductionUnit(ProductionUnit prodUnit)
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
            var recipe = ProductionManager.RecipeProvider.GetRecipeByName(recipeName);

            var block = new ProductionBlock(request, recipe);
            var savedUnits = new List<ProductionUnit>();

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

        private static ProductionUnit DeserializeProductionUnit(string[] savedContent, int index)
        {
            var resource = savedContent[index].Split(' ')[1];
            var countPerMinute = double.Parse(savedContent[index].Split(' ')[2], CultureInfo.InvariantCulture);
            var request = new ResourceRequest(resource, countPerMinute);

            var recipeName = savedContent[index + 1].Split(' ')[1];
            var recipe = ProductionManager.RecipeProvider.GetRecipeByName(recipeName);

            return new ProductionUnit(request, recipe);
        }

        private static void ReconstructProductionBlock(ProductionBlock block, List<ProductionUnit> controlUnitList)
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
                        block.AddProductionUnit(block.Inputs[j], controlUnitList[i].Recipe);
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
                var productionLines = new List<ProductionLine>();
                string[] savedContent = File.ReadAllLines(_filePath);

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
                    (ex.Message + "\n\nНе удалось загрузить ранее сохранённую фабрику. Вы можете создать новую с нуля.",
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
