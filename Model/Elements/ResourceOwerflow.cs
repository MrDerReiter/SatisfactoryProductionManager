﻿using System;

namespace SatisfactoryProductionManager.Model.Elements
{
    public class ResourceOverflow
    {
        public string Resource { get; set; }
        public decimal CountPerMinute { get; set; }


        public ResourceOverflow(string resource, decimal count)
        {
            Resource = resource;
            CountPerMinute = count;
        }


        public ResourceStream ToStream()
        {
            return new ResourceStream(Resource, CountPerMinute);
        }

        public void BalanceToRequest(ResourceRequest request)
        {
            var offcut = Math.Min(CountPerMinute, request.CountPerMinute);
            CountPerMinute -= offcut;
            request.CountPerMinute -= offcut;
        }
    }
}
