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

        return [.. linesArray.Select(lineObject =>
        {
            return new ProductionLine(lineObject["ProductionBlocks"].Select(blockObject =>
            {
                return new ProductionBlock(blockObject["ProductionUnits"].Select(unitObject =>
                {
                    var request = new ResourceStream((string)unitObject["ProductionRequest"]);
                    var recipe = App.RecipeProvider.Get((string)unitObject["Recipe"]);

                    return new SatisfactoryProductionUnit(request, recipe)
                    {
                        Overclock = (double)unitObject["Overclock"],
                        IsSomersloopUsed = (bool)unitObject["IsSomersloopUsed"]
                    };
                }));
            }));
        })];
    }

    public void SaveFactory(List<ProductionLine> lines)
    {
        static JObject ConvertToJObject(ProductionLine line)
        {
            return JObject.FromObject(new
            {
                ProductionBlocks = line.ProductionBlocks.Select(block =>
                {
                    return new
                    {
                        ProductionRequest = block.ProductionRequest.ToString(),
                        ProductionUnits = block.ProductionUnits
                        .Cast<SatisfactoryProductionUnit>()
                        .Select(unit =>
                        {
                            return new
                            {
                                ProductionRequest = unit.ProductionRequest.ToString(),
                                Recipe = unit.Recipe.ToString(),
                                unit.Overclock,
                                unit.IsSomersloopUsed
                            };
                        })
                    };
                })
            });
        }


        JArray linesArray = [.. lines.Select(ConvertToJObject)];
        File.WriteAllText(filePath, linesArray.ToString());
    }
}
