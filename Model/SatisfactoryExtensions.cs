using FactoryManagementCore.Elements;
using FactoryManagementCore.Production;

namespace SatisfactoryProductionManager.Model
{
    public static class SatisfactoryExtensions
    {
        public static void AddProductionBlock(this ProductionLine line, Recipe recipe)
        {
            var resource = (recipe as SatisfactoryRecipe).Product.Resource;
            var request = new ResourceRequest(resource, 0);
            line.AddProductionBlock(request, recipe);
        }

        public static void AddProductionBlock(this ProductionLine line, ResourceRequest request, Recipe recipe)
        {
            var unit = new SatisfactoryProductionUnit(request, (SatisfactoryRecipe)recipe);
            line.AddProductionBlock(unit);
        }

        public static void AddProductionUnit(this ProductionBlock block, ResourceRequest request, Recipe recipe)
        {
            var unit = new SatisfactoryProductionUnit(request, (SatisfactoryRecipe)recipe);
            block.AddProductionUnit(unit);
        }
    }
}
