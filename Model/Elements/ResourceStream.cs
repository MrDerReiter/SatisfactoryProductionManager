using System;

namespace SatisfactoryProductionManager.Model.Elements
{
    public struct ResourceStream
    {
        public readonly string Resource;
        public readonly decimal CountPerMinute;


        public ResourceStream(string resource, decimal count)
        {
            Resource = resource;
            CountPerMinute = count;
        }


        public static ResourceStream operator +(ResourceStream left, ResourceStream right)
        {
            if (left.Resource == right.Resource)
                return new ResourceStream(left.Resource, left.CountPerMinute + right.CountPerMinute);
            else throw new InvalidOperationException("Cannot add streams with different resources.");
        }

        public static ResourceStream operator -(ResourceStream left, ResourceStream right)
        {
            if (left.Resource == right.Resource)
                return new ResourceStream(left.Resource, left.CountPerMinute - right.CountPerMinute);
            else throw new InvalidOperationException("Cannot add streams with different resources.");
        }

        public static ResourceStream operator *(ResourceStream left, decimal right)
        {
            return new ResourceStream(left.Resource, left.CountPerMinute * right);
        }

        public ResourceRequest ToRequest()
        {
            return new ResourceRequest(Resource, CountPerMinute);
        }

        public ResourceOverflow ToOverflow()
        {
            return new ResourceOverflow(Resource, CountPerMinute);
        }
    }
}
