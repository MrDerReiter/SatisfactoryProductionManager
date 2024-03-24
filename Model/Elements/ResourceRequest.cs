using SatisfactoryProductionManager.Model.Production;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SatisfactoryProductionManager.Model.Elements
{
    /// <summary>
    /// Инкапсулирует запрос на ресурс (тип ресурса/количество в минуту).
    /// В отличии от структуры ResourceStream является изменяемым; а также может хранить ссылку на цех, удовлетворяющий запрос. 
    /// </summary>
    public class ResourceRequest
    {
        private ProductionUnit _provider;
        private double _countPerMinute;

        public string Resource {  get; set; }
        public double CountPerMinute
        {
            get => _countPerMinute;
            set
            {
                _countPerMinute = value;
                RequestChanged?.Invoke();
            }
        }
        public ProductionUnit Provider
        {
            get => _provider;
            set
            {
                _provider = value;
                foreach (var request in LinkedRequests)
                    request.Provider = value;
            }
        }
        public List<ResourceRequest> LinkedRequests { get; }
        public bool HasProvider { get => Provider != null; }

        public event Action RequestChanged;


        public ResourceRequest(string resource, double count)
        {
            Resource = resource;
            _countPerMinute = count;
            LinkedRequests = new List<ResourceRequest>();
        }


        private void Update()
        {
            double totalCountPerMinute = 0;

            for (int i = 0; i < LinkedRequests.Count; i++)
            {
                if (LinkedRequests[i].CountPerMinute == 0)
                    LinkedRequests.RemoveAt(i--);
                else totalCountPerMinute += LinkedRequests[i].CountPerMinute;
            }

            CountPerMinute = totalCountPerMinute;
        }


        public static bool operator ==(ResourceRequest left, ResourceRequest right)
        {
            return left.ToString() == right.ToString();
        }

        public static bool operator !=(ResourceRequest left, ResourceRequest right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return Resource + " " + CountPerMinute.ToString(CultureInfo.InvariantCulture);
        }

        public ResourceStream ToStream()
        {
            return new ResourceStream(Resource, CountPerMinute);
        }

        public void Link(ResourceRequest request)
        {
            if (request.Resource != Resource) throw new InvalidOperationException("Cannot link recipies with different resources");

            LinkedRequests.Add(request);
            request.RequestChanged += Update;
        }
    }
}
