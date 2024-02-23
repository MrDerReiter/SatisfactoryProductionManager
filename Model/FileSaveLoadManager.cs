using Newtonsoft.Json;
using SatisfactoryProductionManager.Model.Interfaces;
using SatisfactoryProductionManager.Model.Production;
using System;
using System.Collections.Generic;
using System.IO;

namespace SatisfactoryProductionManager.Model
{
    public class FileSaveLoadManager : IFactorySaveLoadManager
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\ProductionLines.json";

        public List<ProductionLine> LoadFactory()
        {
            string content = File.ReadAllText(_path);
            var list = JsonConvert.DeserializeObject<List<ProductionLine>>(content);
            return list;
        }

        public void SaveFactory()
        {
            var list = new List<ProductionLine>(ProductionManager.ProductionLines);
            string content = JsonConvert.SerializeObject(list);
            File.WriteAllText(_path, content);
        }
    }
}
