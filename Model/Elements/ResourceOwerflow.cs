using System;

namespace SatisfactoryProductionManager.Model.Elements
{
    public class ResourceOverflow
    {
        public string Resource { get; set; }
        public double CountPerMinute { get; set; }


        public ResourceOverflow(string resource, double count)
        {
            Resource = resource;
            CountPerMinute = count;
        }


        public ResourceStream ToStream()
        {
            return new ResourceStream(Resource, CountPerMinute);
        }
    }
}
