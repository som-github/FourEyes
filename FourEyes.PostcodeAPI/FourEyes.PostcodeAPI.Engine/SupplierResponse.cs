namespace FourEyes.PostcodeAPI.Engine
{
    public class SupplierResponse
    {
        public int status { get; set; }
        public Result result { get; set; }

    }

    public class Result
    {
        public string postcode { get; set; }

        public string longitude { get; set; }

        public string latitude { get; set; }
    }
}
