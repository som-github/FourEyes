using FourEyes.PostcodeAPI.Interfaces;
using LazyCache;
using System;

namespace FourEyes.PostcodeAPI.Cache
{
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
