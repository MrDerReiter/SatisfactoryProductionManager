﻿using SatisfactoryProductionManager.Model.Elements;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SatisfactoryProductionManager.Model.Production
{
    public class ProductionLine
    {
        public List<ProductionBlock> ProductionBlocks { get; } = new List<ProductionBlock>();
        public ProductionBlock MainProductionBlock { get => ProductionBlocks[0]; }
        public List<ResourceStream> Inputs { get; } = new List<ResourceStream>();
        public List<ResourceStream> Outputs { get; } = new List<ResourceStream>();


        [JsonConstructor]
        public ProductionLine()
        {

        }

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
                for(int j = 0; j < Inputs.Count; j++)
                    if (Outputs[i].Resource == Inputs[j].Resource)
                    {
                        if(Outputs[i].CountPerMinute < Inputs[j].CountPerMinute)
                        {
                            Inputs[j] = Inputs[j] - Outputs[i];
                            Outputs.RemoveAt(i);
                            i--;
                        }
                        else if(Outputs[i].CountPerMinute > Inputs[j].CountPerMinute)
                        {
                            Outputs[i] = Outputs[i] - Inputs[j];
                            Inputs.RemoveAt(j);
                            j--;
                        }
                        else
                        {
                            Outputs.RemoveAt(i);
                            Inputs.RemoveAt(j);
                            i--;
                            j--;
                        }
                    }
        }

        private void UpdateIO()
        {
            Inputs.Clear();
            Inputs.AddRange(ProductionBlocks.SelectMany(pb => pb.TotalInput));
            
            Outputs.Clear();
            Outputs.AddRange(ProductionBlocks.SelectMany(pb => pb.TotalOutput));

            OptimizeIO();
        }


        public void AddProductionBlock(Recipe recipe)
        {
            var prodBlock = new ProductionBlock(recipe);
            ProductionBlocks.Add(prodBlock);
            prodBlock.ProductionRequest.RequestChanged += UpdateIO;
        }

        public void AddProductionBlock(ProductionUnit productionUnit)
        {
            var prodBlock = new ProductionBlock(productionUnit);
            ProductionBlocks.Add(prodBlock);
            prodBlock.ProductionRequest.RequestChanged += UpdateIO;
        }

        public void RemoveProductionBlock(ProductionBlock prodBlock)
        {
            ProductionBlocks.Remove(prodBlock);
        }
    }
}
