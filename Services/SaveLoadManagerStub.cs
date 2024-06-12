using FactoryManagementCore.Interfaces;
using FactoryManagementCore.Production;
using System.Collections.Generic;

namespace SatisfactoryProductionManager.Services
{
    public class SaveLoadManagerStub : IFactorySaveLoadManager
    {
        public void SaveFactory() { }

        public List<ProductionLine> LoadFactory()
        {
            return new List<ProductionLine>();
        }
    }
}
