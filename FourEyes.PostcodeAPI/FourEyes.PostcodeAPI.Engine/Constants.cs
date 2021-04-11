namespace FourEyes.PostcodeAPI.Engine
{
    public class Constants
    {
        public const string DATA_CONTRACT_NAMESPACE = "http://foureyes.postcodeapi.com";

        public const string POSTCODE_IO_BASE_URL = "PostcodeIOBaseUrl";

        public enum SupplierErrorCodes
        {
            Success = 200,
            Invalid = 404,
            Blank = 400
        }
    }
}
