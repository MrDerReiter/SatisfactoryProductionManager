using FactoryManagementCore.Production;
using FactoryManagementCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using FactoryManagementCore.Elements;
using SatisfactoryProductionManager.Model;
using System.Linq;
using System.Text.RegularExpressions;

namespace SatisfactoryProductionManager.Services
{
    public class SatisfactoryXMLSaveLoadManager : IFactorySaveLoadManager
    {
        private readonly string _filePath = "ProductionLines.cfg";
        private readonly int _indentStep = 2;


        private static string MakeIndent(int indent)
        {
            return new string(' ', indent);
        }


        public List<ProductionLine> LoadFactory()
        {
            static ProductionLine ReadProductionLine(string[] savedContent, int index, out int endLine)
            {
                static ProductionBlock ReadProductionBlock
                    (string[] savedContent, int index, out int endBlock)
                {
                    #region Локальные функции
                    static SatisfactoryProductionUnit ReadProductionUnit
                        (string[] savedContent, int index, out int endUnit)
                    {
                        var requestPattern = new Regex(@"\w+\s\d+(\.\d+)?");
                        var stringifiedRequest =
                            requestPattern.Match(savedContent[index]).Value.Split(' ');
                        var request =
                            new ResourceRequest
                            (stringifiedRequest[0],
                             double.Parse(stringifiedRequest[1], CultureInfo.InvariantCulture));

                        var recipeName = savedContent[++index]
                            .Split('=')[1]
                            .Replace("\"", string.Empty);
                        var recipe = ProductionManager.RecipeProvider.GetRecipeByName(recipeName);

                        var overclock = double.Parse(savedContent[++index]
                             .Split('=')[1]
                             .Replace("\"", string.Empty), CultureInfo.InvariantCulture);

                        var isSomersloopUsed = Convert.ToBoolean
                            (savedContent[++index]
                            .Split('=')[1]
                            .Replace("\"", string.Empty)
                            .Replace("/>", string.Empty));

                        var unit = new SatisfactoryProductionUnit
                            (request, (SatisfactoryRecipe)recipe)
                        {
                            Overclock = overclock,
                            IsSomersloopUsed = isSomersloopUsed
                        };

                        endUnit = index;
                        return unit;
                    }

                    static ProductionBlock ReconstructProductionBlock
                        (List<SatisfactoryProductionUnit> controlUnitList,
                        ResourceRequest request, Recipe recipe)
                    {
                        if (controlUnitList[0].ProductionRequest != request ||
                            controlUnitList[0].Recipe != recipe)
                            throw new InvalidDataException
                                ("Ошибка в сохранённых данных. " +
                                 "Главный цех производственного блока " +
                                 "не совпадает с его сигнатурой.");

                        var block = new ProductionBlock(controlUnitList[0]);
                        controlUnitList.RemoveAt(0);

                        for (int i = 0; i < controlUnitList.Count; i++)
                        {
                            for (int j = 0; j < block.Inputs.Count; j++)
                            {
                                if (controlUnitList[i].ProductionRequest == block.Inputs[j])
                                {
                                    var unit = controlUnitList[i]
                                        .CloneWithNewRequestInstance(block.Inputs[j]);

                                    block.AddProductionUnit(unit);
                                    controlUnitList.RemoveAt(i);
                                    i = -1;
                                    break;
                                }
                            }
                        }

                        return block;
                    }
                    #endregion

                    var requestPattern = new Regex(@"\w+\s\d+(\.\d+)?");
                    var stringifiedRequest =
                        requestPattern.Match(savedContent[index]).Value.Split(' ');
                    var request =
                        new ResourceRequest
                        (stringifiedRequest[0],
                         double.Parse(stringifiedRequest[1], CultureInfo.InvariantCulture));

                    var recipeName = savedContent[++index]
                        .Split('=')[1]
                        .Replace("\"", string.Empty)
                        .Replace(">", string.Empty);
                    var recipe = ProductionManager.RecipeProvider.GetRecipeByName(recipeName);
                    index++;

                    var savedUnits = new List<SatisfactoryProductionUnit>();
                    var unitPattern = new Regex(@"<ProductionUnit ");
                    var endBlockPattern = new Regex(@"</ProductionBlock>$");
                    for (int i = index; i < savedContent.Length; i++)
                    {
                        if (unitPattern.IsMatch(savedContent[i]))
                        {
                            var unit = ReadProductionUnit(savedContent, i, out int endUnit);
                            savedUnits.Add(unit);
                            i = endUnit;
                        }
                        else if (endBlockPattern.IsMatch(savedContent[i]))
                        {
                            endBlock = i;
                            return ReconstructProductionBlock(savedUnits, request, recipe);
                        }
                    }

                    throw new InvalidDataException
                            ("Ошибка в сохранённых данных. " +
                             "Отсутствует закрывающий тег </ProductionBlock>");
                }


                var line = new ProductionLine();
                var blockPattern = new Regex("<ProductionBlock ");
                var endLinePattern = new Regex("</ProductionLine>$");

                for (int i = index; i < savedContent.Length; i++)
                {
                    if (blockPattern.IsMatch(savedContent[i]))
                    {
                        var block = ReadProductionBlock(savedContent, i, out int endBlock);
                        line.AddProductionBlock(block);
                        i = endBlock;
                    }
                    else if (endLinePattern.IsMatch(savedContent[i]))
                    {
                        endLine = i;
                        return line;
                    }
                }

                throw new InvalidDataException
                    ("Ошибка в сохранённых данных. " +
                     "Отсутствует закрывающий тег </ProductionLine>");
            }


            if (!File.Exists(_filePath)) return [];

            string[] savedContent = File.ReadAllLines(_filePath);
            if (savedContent is ["<Factory>", "</Factory>"] ||
                savedContent.Length == 0) return [];

            var result = new List<ProductionLine>();
            var linePattern = new Regex("<ProductionLine>$");

            for (int i = 0; i < savedContent.Length; i++)
            {
                if (linePattern.IsMatch(savedContent[i]))
                {
                    var line = ReadProductionLine(savedContent, ++i, out int endLine);
                    result.Add(line);
                    i = endLine;
                }
            }

            return result;
        }

        public void SaveFactory()
        {
            List<string> StringifyProductionLine(ProductionLine prodLine, int indent)
            {
                List<string> StringifyProductionBlock
                    (ProductionBlock prodBlock, int indent)
                {
                    List<string> StringifyProductionUnit
                        (SatisfactoryProductionUnit prodUnit, int indent)
                    {
                        List<string> stringifiedUnit =
                            [MakeIndent(indent) +
                            $"<ProductionUnit ProductionRequest=\"{prodUnit.ProductionRequest}\""];

                        var tagIndent = indent + "<ProductionUnit ".Length;
                        stringifiedUnit.Add
                            (MakeIndent(tagIndent) + $"Recipe=\"{prodUnit.Recipe.Name}\"");
                        stringifiedUnit.Add
                            (MakeIndent(tagIndent) + $"Overclock=\"{prodUnit.Overclock}\"");
                        stringifiedUnit.Add
                            (MakeIndent(tagIndent) +
                            $"IsSomersloopUsed=\"{prodUnit.IsSomersloopUsed}\"/>");

                        return stringifiedUnit;
                    }


                    List<string> stringifiedBlock =
                        [MakeIndent(indent) +
                         $"<ProductionBlock ProductionRequest=\"{prodBlock.ProductionRequest}\""];

                    int tagIndent = indent + "<ProductionBlock ".Length;
                    stringifiedBlock.Add
                        (MakeIndent(tagIndent) +
                        $"Recipe=\"{prodBlock.MainProductionUnit.Recipe.Name}\">");
                    stringifiedBlock.Add(string.Empty);

                    foreach (var unit in
                        prodBlock.ProductionUnits.Cast<SatisfactoryProductionUnit>())
                        stringifiedBlock.AddRange
                            (StringifyProductionUnit(unit, indent + _indentStep));

                    stringifiedBlock.Add(MakeIndent(indent) + "</ProductionBlock>");

                    return stringifiedBlock;
                }


                List<string> stringifiedLine = [MakeIndent(indent) + "<ProductionLine>"];

                foreach (var block in prodLine.ProductionBlocks)
                    stringifiedLine.AddRange(StringifyProductionBlock(block, indent + _indentStep));

                stringifiedLine.Add(MakeIndent(indent) + "</ProductionLine>");

                return stringifiedLine;
            }


            List<string> stringifiedFactory = ["<Factory>"];

            foreach (var line in ProductionManager.ProductionLines)
            {
                var stringifiedLine = StringifyProductionLine(line, _indentStep);
                stringifiedFactory.AddRange(stringifiedLine);
            }
            stringifiedFactory.Add("</Factory>");

            File.WriteAllLines(_filePath, stringifiedFactory);
        }
    }
}
