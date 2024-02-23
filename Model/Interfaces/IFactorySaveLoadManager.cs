using SatisfactoryProductionManager.Model.Production;
using System.Collections.Generic;

namespace SatisfactoryProductionManager.Model.Interfaces
{
    public interface IFactorySaveLoadManager
    {
        List<ProductionLine> LoadFactory();

        void SaveFactory();
    }
}
