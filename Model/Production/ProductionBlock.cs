using SatisfactoryProductionManager.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryProductionManager.Model.Production
{
    public class ProductionBlock
    {
        private readonly List<ResourceRequest> _inputs = new List<ResourceRequest>();
        private readonly List<ResourceStream> _byproducts = new List<ResourceStream>();
        private readonly List<ProductionUnit> _productionUnits = new List<ProductionUnit>();

        public IReadOnlyList<ProductionUnit> ProductionUnits { get => _productionUnits; }
        public ProductionUnit MainProductionUnit { get => _productionUnits[0]; }
        public ResourceRequest ProductionRequest { get; }
        public IReadOnlyList<ResourceRequest> Inputs { get => _inputs; }
        public IReadOnlyList<ResourceStream> Byproducts { get => _byproducts; }
        public IEnumerable<ResourceStream> TotalInput { get => _inputs.Select(i => i.ToStream()); }
        public IEnumerable<ResourceStream> TotalOutput
        {
            get
            {
                yield return ProductionRequest.ToStream();
                foreach (var byproduct in _byproducts)
                    yield return byproduct;
            }
        }

        public event Action IOChanged;


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

        public ProductionBlock(ResourceRequest request, Recipe recipe)
        {
            ProductionRequest = request;
            AddProductionUnit(ProductionRequest, recipe);

            ProductionRequest.Provider = MainProductionUnit;
            ProductionRequest.RequestChanged += UpdateIO;
        }

        
        private void MergeInputs(int firstIndex, int secondIndex)
        {
            var mergedInput = new ResourceRequest
                (_inputs[firstIndex].Resource,
                 _inputs[firstIndex].CountPerMinute + _inputs[secondIndex].CountPerMinute);
            mergedInput.Link(_inputs[firstIndex]);
            mergedInput.Link(_inputs[secondIndex]);

            _inputs[firstIndex] = mergedInput;
            _inputs.RemoveAt(secondIndex);
        }

        private void MergeByproducts(int firstIndex, int secondIndex)
        {
            var mergedByproduct = new ResourceStream
                (_byproducts[firstIndex].Resource,
                 _byproducts[firstIndex].CountPerMinute + _byproducts[secondIndex].CountPerMinute);

            _byproducts[firstIndex] = mergedByproduct;
            _byproducts.RemoveAt(secondIndex);
        }

        private void MergeExcessInputs()
        {
            for (int i = 0; i < _inputs.Count - 1; i++)
                for (int j = i + 1; j < _inputs.Count; j++)
                    if (_inputs[i].Resource == _inputs[j].Resource)
                    {
                        MergeInputs(i, j);
                        j--;
                    }
        }

        private void MergeExcessByproducts()
        {
            for (int i = 0; i < _byproducts.Count - 1; i++)
                for (int j = i + 1; j < _byproducts.Count; j++)
                    if (_byproducts[i].Resource == _byproducts[j].Resource)
                    {
                        MergeByproducts(i, j);
                        j--;
                    }
        }

        private void UpdateIO()
        {
            _inputs.Clear();
            var inputs = _productionUnits
                .SelectMany(pu => pu.Inputs)
                .Where(i => !i.HasProvider && i.CountPerMinute > 0)
                .ToList();
            _inputs.AddRange(inputs);
            MergeExcessInputs();

            _byproducts.Clear();
            var byproducts = _productionUnits
                .Where(pu => pu.HasByproduct && pu.Byproduct.CountPerMinute > 0)
                .Select(pu => pu.Byproduct);
            _byproducts.AddRange(byproducts);
            MergeExcessByproducts();

            IOChanged?.Invoke();
        }


        public void AddProductionUnit(ResourceRequest request, Recipe recipe)
        {
            var unit = new ProductionUnit(request, recipe);
            request.Provider = unit;
            _productionUnits.Add(unit);
            UpdateIO();
        }

        public void AddProductionUnit(ProductionUnit unit)
        {
            unit.ProductionRequest.Provider = unit;
            _productionUnits.Add(unit);
            UpdateIO();
        }

        public void RemoveProductionUnit(ProductionUnit unit)
        {
            if (unit == MainProductionUnit)
                throw new InvalidOperationException("Невозможно удалить или преобразовать главный цех производственного блока.");

            unit.ProductionRequest.Provider = null;
            foreach (var request in unit.Inputs)
                request.CountPerMinute = 0;
            _inputs.Clear();

            _productionUnits.Remove(unit);
            UpdateIO();
        }
    }
}
