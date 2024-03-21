using SatisfactoryProductionManager.Model.Interfaces;
using SatisfactoryProductionManager.Model.Production;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SatisfactoryProductionManager.Model
{
    public class FileSaveLoadManager : IFactorySaveLoadManager
    {
        private readonly string _filePath = $"{Environment.CurrentDirectory}\\ProductionLines.txt";


        private List<string> SerializeProductionLine(ProductionLine prodLine)
        {
            List<string> serializedLine = ["ProductionLine:", string.Empty];

            foreach (var block in prodLine.ProductionBlocks)
                serializedLine.AddRange(SerializeProductionBlock(block));

            return serializedLine;
        }

        private List<string> SerializeProductionBlock(ProductionBlock prodBlock)
        {
            List<string> serializedBlock = ["\tProductionBlock:", string.Empty];

            serializedBlock.Add($"\tProductionRequest: {prodBlock.ProductionRequest}");
            serializedBlock.Add($"\tRecipe: {prodBlock.MainProductionUnit.Recipe.Name}");
            serializedBlock.Add(string.Empty);

            foreach (var unit in prodBlock.ProductionUnits)
                serializedBlock.AddRange(SerializeProductionUnit(unit));

            return serializedBlock;
        }

        private List<string> SerializeProductionUnit(ProductionUnit prodUnit)
        {
            List<string> serializedUnit = ["\t\tProductionUnit:", string.Empty];

            serializedUnit.Add($"\t\tID: {prodUnit.Id}");
            serializedUnit.Add($"\t\tRecipe: {prodUnit.Recipe.Name}");
            serializedUnit.Add($"\t\tMachine: {prodUnit.Machine}");
            serializedUnit.Add($"\t\tMachinesCount: {prodUnit.MachinesCount.ToString(CultureInfo.InvariantCulture)}");
            serializedUnit.Add($"\t\tProductionRequest: {prodUnit.ProductionRequest}");
            serializedUnit.Add(string.Empty);

            return serializedUnit;
        }


        public List<ProductionLine> LoadFactory()
        {
            throw new NotImplementedException();
        }

        public void SaveFactory()
        {
            var allProductionLines = new List<string>();

            foreach (var line in ProductionManager.ProductionLines)
            {
                var paragraph = SerializeProductionLine(line);
                allProductionLines.AddRange(paragraph);
                allProductionLines.AddRange([string.Empty, string.Empty]);
            }

            File.WriteAllLines(_filePath, allProductionLines);
        }
    }
}
