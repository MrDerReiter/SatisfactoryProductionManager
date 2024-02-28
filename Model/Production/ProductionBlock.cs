using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryProductionManager.Model.Production
{
    public class ProductionBlock
    {
        public List<ProductionUnit> ProductionUnits { get; } = new List<ProductionUnit>();
        public ProductionUnit MainProductionUnit { get => ProductionUnits[0]; }
        public ResourceRequest ProductionRequest { get; }
        public List<ResourceRequest> Inputs { get; } = new List<ResourceRequest>();
        public List<ResourceOverflow> Byproducts { get; } = new List<ResourceOverflow>();
        public IEnumerable<ResourceStream> TotalInput { get => Inputs.Select(i => i.ToStream()); }
        public IEnumerable<ResourceStream> TotalOutput
        {
            get
            {
                yield return ProductionRequest.ToStream();
                foreach (var byproduct in Byproducts)
                    yield return byproduct.ToStream();
            }
        }


        public ProductionBlock(Recipe recipe)
        {
            var resource = recipe.Product.Resource;
            ProductionRequest = new ResourceRequest(resource, 0);
            AddProductionUnit(ProductionRequest, recipe);

            ProductionRequest.Provider = MainProductionUnit;
            ProductionRequest.RequestChanged += UpdateIO;
        }

        public ProductionBlock(ProductionUnit unit)
        {
            var request = new ResourceRequest(unit.ProductionRequest.Resource, unit.ProductionRequest.CountPerMinute);
            var recipe = unit.Recipe;

            ProductionRequest = request;
            AddProductionUnit(ProductionRequest, recipe);

            ProductionRequest.Provider = MainProductionUnit;
            ProductionRequest.RequestChanged += UpdateIO;
        }


        private void MergeInputs(int firstIndex, int secondIndex)
        {
            var mergedInput = new ResourceRequest
                (Inputs[firstIndex].Resource,
                 Inputs[firstIndex].CountPerMinute + Inputs[secondIndex].CountPerMinute);
            mergedInput.Link(Inputs[firstIndex]);
            mergedInput.Link(Inputs[secondIndex]);

            Inputs[firstIndex] = mergedInput;
            Inputs.RemoveAt(secondIndex);
        }

        private void MergeByproducts(int firstIndex, int secondIndex)
        {
            var mergedByproduct = new ResourceOverflow
                (Byproducts[firstIndex].Resource,
                 Byproducts[firstIndex].CountPerMinute + Byproducts[secondIndex].CountPerMinute);

            Byproducts[firstIndex] = mergedByproduct;
            Byproducts.RemoveAt(secondIndex);
        }

        private void MergeExcessInputs()
        {
            for (int i = 0; i < Inputs.Count - 1; i++)
                for (int j = i + 1; j < Inputs.Count; j++)
                    if (Inputs[i].Resource == Inputs[j].Resource)
                    {
                        MergeInputs(i, j);
                        j--;
                    }
        }

        private void MergeExcessByproducts()
        {
            for (int i = 0; i < Byproducts.Count - 1; i++)
                for (int j = i + 1; j < Byproducts.Count; j++)
                    if (Byproducts[i].Resource == Byproducts[j].Resource)
                    {
                        MergeByproducts(i, j);
                        j--;
                    }
        }

        private void UpdateIO()
        {
            Inputs.Clear();
            var inputs = ProductionUnits
                .SelectMany(pu => pu.Inputs)
                .Where(i => !i.HasProvider && i.CountPerMinute > 0)
                .ToList();
            Inputs.AddRange(inputs);
            MergeExcessInputs();

            Byproducts.Clear();
            var byproducts = ProductionUnits
                .Where(pu => pu.HasByproduct && pu.Byproduct.CountPerMinute > 0)
                .Select(pu => pu.Byproduct);
            Byproducts.AddRange(byproducts);
            MergeExcessByproducts();
        }


        public void AddProductionUnit(ResourceRequest request, Recipe recipe)
        {
            if (request.Resource != recipe.Product.Resource) throw new InvalidOperationException("The production unit's recipe doesn't match the request");

            var unit = new ProductionUnit(request, recipe);
            request.Provider = unit;
            ProductionUnits.Add(unit);
            UpdateIO();
        }

        public void RemoveProductionUnit(ProductionUnit unit)
        {
            if (unit == MainProductionUnit)
                throw new InvalidOperationException("Невозможно удалить или преобразовать главный цех производственного блока.");

            unit.ProductionRequest.Provider = null;
            foreach (var request in unit.Inputs)
                request.CountPerMinute = 0;
            Inputs.Clear();

            ProductionUnits.Remove(unit);
            UpdateIO();
        }
    }
}
