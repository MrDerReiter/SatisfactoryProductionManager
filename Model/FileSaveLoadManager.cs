using SatisfactoryProductionManager.Model.Interfaces;
using SatisfactoryProductionManager.Model.Production;
using System;
using System.Collections.Generic;

namespace SatisfactoryProductionManager.Model
{
    public class FileSaveLoadManager : IFactorySaveLoadManager
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\ProductionLines.txt";

        public List<ProductionLine> LoadFactory()
        {
            throw new NotImplementedException();
        }

        public void SaveFactory()
        {
            throw new NotImplementedException();
        }
    }
}
