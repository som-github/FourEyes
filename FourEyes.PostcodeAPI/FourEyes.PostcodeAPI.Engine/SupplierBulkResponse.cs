using System.Collections.Generic;

namespace FourEyes.PostcodeAPI.Engine
{
    public class SupplierBulkResponse
    {
        public int status { get; set; }

        public List<BulkResult> result { get; set; }
    }

    public class BulkResult
    {
        public string query { get; set; }
        public Result result { get; set; }
    }
}
