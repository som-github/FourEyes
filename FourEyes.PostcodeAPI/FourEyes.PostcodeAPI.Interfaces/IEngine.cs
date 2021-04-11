namespace FourEyes.PostcodeAPI.Interfaces
{
    public interface IEngine
    {
        bool VerifyAPIAccessibility(string baseUrl);

        IResponse GetSinglePostcodeInformation(string postcode, string supplierBaseUrl);

        IResponse GetMultiPostcodesInformation(string postcodes, string supplierBaseUrl);
    }
}
