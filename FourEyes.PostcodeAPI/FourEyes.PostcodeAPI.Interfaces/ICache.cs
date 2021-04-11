namespace FourEyes.PostcodeAPI.Interfaces
{
    public interface ICache
    {
        IResponse CacheSinglePostcodeRequest(string postcode, string supplierBaseUrl);

        IResponse CacheMultiPostcodeRequest(string postcodes, string supplierBaseUrl);
    }
}
