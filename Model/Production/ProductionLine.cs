using SatisfactoryProductionManager.Model.Elements;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryProductionManager.Model.Production
{
    public class ProductionLine
    {
        private readonly List<ProductionBlock> _productionBlocks = new List<ProductionBlock>();

        public IReadOnlyList<ProductionBlock> ProductionBlocks { get => _productionBlocks; }
        public ProductionBlock MainProductionBlock { get => _productionBlocks[0]; }
        public List<ResourceStream> Inputs { get; } = new List<ResourceStream>();
        public List<ResourceStream> Outputs { get; } = new List<ResourceStream>();


        public ProductionLine() { }

        public ProductionLine(Recipe recipe)
        {
            AddProductionBlock(recipe);
        }


        private void MergeExcessIO()
        {
            for (int i = 0; i < Inputs.Count - 1; i++)
                for (int j = i + 1; j < Inputs.Count; j++)
                    if (Inputs[i].Resource == Inputs[j].Resource)
                    {
                        Inputs[i] = Inputs[i] + Inputs[j];
                        Inputs.RemoveAt(j);
                        j--;
                    }

            for (int i = 0; i < Outputs.Count - 1; i++)
                for (int j = i + 1; j < Outputs.Count; j++)
                    if (Outputs[i].Resource == Outputs[j].Resource)
                    {
                        Outputs[i] = Outputs[i] + Outputs[j];
                        Outputs.RemoveAt(j);
                        j--;
                    }
        }

        private void OptimizeIO()
        {
            MergeExcessIO();

            for (int i = 0; i < Outputs.Count; i++)
                for (int j = 0; j < Inputs.Count; j++)
                    if (Outputs[i].Resource == Inputs[j].Resource)
                    {
                        if (Outputs[i].CountPerMinute < Inputs[j].CountPerMinute)
                        {
                            Inputs[j] = Inputs[j] - Outputs[i];
                            Outputs.RemoveAt(i);
                            if (i > 0) i--;
                        }
                        else if (Outputs[i].CountPerMinute > Inputs[j].CountPerMinute)
                        {
                            Outputs[i] = Outputs[i] - Inputs[j];
                            Inputs.RemoveAt(j);
                            j--;
                        }
                        else
                        {
                            Outputs.RemoveAt(i);
                            Inputs.RemoveAt(j);
                            if (i > 0) i--;
                            j--;
                        }
                    }
        }

        private void UpdateIO()
        {
            Inputs.Clear();
            Inputs.AddRange(_productionBlocks.SelectMany(pb => pb.TotalInput));

            Outputs.Clear();
            Outputs.AddRange(_productionBlocks.SelectMany(pb => pb.TotalOutput));

            OptimizeIO();
        }


        public void AddProductionBlock(Recipe recipe)
        {
            var prodBlock = new ProductionBlock(recipe);
            _productionBlocks.Add(prodBlock);
            prodBlock.IOChanged += UpdateIO;
        }

        public void AddProductionBlock(ProductionUnit prodUnit)
        {
            var prodBlock = new ProductionBlock(prodUnit);
            _productionBlocks.Add(prodBlock);
            prodBlock.IOChanged += UpdateIO;

            UpdateIO();
        }

        public void AddProductionBlock(ProductionBlock prodBlock)
        {
            _productionBlocks.Add(prodBlock);
            prodBlock.IOChanged += UpdateIO;

            UpdateIO();
        }

        public void AddProductionBlock(ResourceRequest request, Recipe recipe)
        {
            var prodBlock = new ProductionBlock(request, recipe);
            _productionBlocks.Add(prodBlock);
            prodBlock.IOChanged += UpdateIO;

            UpdateIO();
        }

        public void RemoveProductionBlock(ProductionBlock prodBlock)
        {
            prodBlock.IOChanged -= UpdateIO;
            _productionBlocks.Remove(prodBlock);
            UpdateIO();
        }
    }
}
