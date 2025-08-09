using FactoryManagementCore;
using Newtonsoft.Json.Linq;
using System.IO;


namespace SatisfactoryProductionManager;

public class DefaultSaveLoadManager(string filePath) : IFactoryKeeper
{
    public List<ProductionLine> LoadFactory()
    {
        if (!File.Exists(filePath)) return [];
        var linesArray = JArray.Parse(File.ReadAllText(filePath));

        List<ProductionLine> lines =
        [
            ..linesArray.Select(lineJOblect =>
            {
                var blocks = lineJOblect["ProductionBlocks"]
                .Select(blockJObject =>
                {
                    var units = blockJObject["ProductionUnits"]
                    .Select(unitJObject =>
                    {
                        var request = new ResourceStream((string)unitJObject["ProductionRequest"]);
                        var recipe = App.RecipeProvider.Get((string)unitJObject["Recipe"]);

                        var unit = new SatisfactoryProductionUnit(request, recipe)
                        { 
                            Overclock = (double)unitJObject["Overclock"],
                            IsSomersloopUsed = (bool)unitJObject["IsSomersloopUsed"]
                        };

                        return unit;
                    });

                    return new ProductionBlock(units);
                });

                return new ProductionLine(blocks);
            })
        ];

        return lines;
    }

    public void SaveFactory(List<ProductionLine> lines)
    {
        static JObject ConvertToJObject(ProductionLine line)
        {
            return new JObject
            {
                {
                    "ProductionBlocks",
                    new JArray(line.ProductionBlocks
                    .Select(block =>
                    {
                        return new JObject
                        {
                            { "ProductionRequest", block.ProductionRequest.ToString() },
                            {
                                "ProductionUnits",
                                new JArray(block.ProductionUnits
                                .Cast<SatisfactoryProductionUnit>()
                                .Select(unit =>
                                {
                                    return new JObject
                                    {
                                        { "ProductionRequest", unit.ProductionRequest.ToString() },
                                        { "Recipe", unit.Recipe.ToString() },
                                        { "Overclock", unit.Overclock },
                                        { "IsSomersloopUsed", unit.IsSomersloopUsed }
                                    };
                                }).ToArray())
                            }
                        };
                    }).ToArray())
                }
            };
        }


        JArray linesArray = [.. lines.Select(ConvertToJObject)];
        File.WriteAllText(filePath, linesArray.ToString());
    }
}
