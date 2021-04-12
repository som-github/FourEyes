using FourEyes.PostcodeAPI.Interfaces;
using LazyCache;
using System;

namespace FourEyes.PostcodeAPI.Cache
{
    /**********************************************************************
     * This caching class provides the Caching for API via its Engine. It
     * wraps the Caching Mechanism and that makes it a scalable component 
     * if we changes the Caching Technology or Mechanism without impacting
     * the Application. This component is plugable via the ICache interface.
     **********************************************************************/
    public class CacheFacade : ICache
    {
        private IAppCache Cache { get; }

        public Func<string, string, IResponse> SinglePostcodeServiceCallback { get; set; }

        public Func<string, string, IResponse> MultiPostcodeServiceCallback { get; set; }

        public CacheFacade()
        {
            Cache = new CachingService();
        }

        public IResponse CacheSinglePostcodeRequest(string postcode, string supplierBaseUrl)
        {
            string key = string.Empty;
            if (string.IsNullOrWhiteSpace(postcode))
            {
                key = "|"; //Should be avoided for valid Input
            }
            else
            {
                key = postcode;
            }

            return Cache.GetOrAdd(key, () => SinglePostcodeServiceCallback(postcode, supplierBaseUrl));
        }

        public IResponse CacheMultiPostcodeRequest(string postcodes, string supplierBaseUrl)
        {
            string key = string.Empty;
            if(string.IsNullOrWhiteSpace(postcodes))
            {
                key = "|"; //Should be avoided for valid Input
            }
            else
            {
                key = postcodes;
            }

            return Cache.GetOrAdd(key, () => MultiPostcodeServiceCallback(postcodes, supplierBaseUrl));
        }
    }
}
