using FactoryManagementCore.Elements;
using FactoryManagementCore.Production;

namespace SatisfactoryProductionManager.Model
{
    public static class ProductionExtensions
    {
        public static void AddProductionBlock
            (this ProductionLine line, ResourceRequest request, SatisfactoryRecipe recipe)
        {
            var unit = new SatisfactoryProductionUnit(request, recipe);
            var block = new ProductionBlock(unit);
            line.AddProductionBlock(block);
        }

        public static void AddProductionBlock(this ProductionLine line, SatisfactoryRecipe recipe)
        {
            var request = new ResourceRequest(recipe.Product.Resource, 0);
            line.AddProductionBlock(request, recipe);
        }

        public static void AddProductionUnit
            (this ProductionBlock block, ResourceRequest request, SatisfactoryRecipe recipe)
        {
            var unit = new SatisfactoryProductionUnit(request, recipe);
            block.AddProductionUnit(unit);
        }
    }
}
