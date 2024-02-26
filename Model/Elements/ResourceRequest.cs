using SatisfactoryProductionManager.Model.Production;
using System;

namespace SatisfactoryProductionManager.Model.Elements
{
    /// <summary>
    /// Инкапсулирует запрос на ресурс (тип ресурса/количество в минуту).
    /// В отличии от структуры ResourceStream является изменяемым; а также может хранить ссылку на цех, удовлетворяющий запрос. 
    /// </summary>
    public class ResourceRequest
    {
        private decimal _countPerMinute;

        public string Resource {  get; set; }
        public decimal CountPerMinute
        {
            get => _countPerMinute;
            set
            {
                _countPerMinute = value;
                RequestChanged?.Invoke();
            }
        }
        public ProductionUnit Provider { get; set; }
        public bool HasProvider { get => Provider != null; }

        public event Action RequestChanged;


        public ResourceRequest(string resource, decimal count)
        {
            Resource = resource;
            _countPerMinute = count;
        }


        public ResourceStream ToStream()
        {
            return new ResourceStream(Resource, CountPerMinute);
        }
    }
}
