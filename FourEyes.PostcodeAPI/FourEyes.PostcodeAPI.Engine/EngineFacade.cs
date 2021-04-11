using FourEyes.PostcodeAPI.Engine.DTO;
using FourEyes.PostcodeAPI.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using System.Collections.Generic;
using static FourEyes.PostcodeAPI.Engine.Constants;
using FourEyes.PostcodeAPI.Cache;

namespace FourEyes.PostcodeAPI.Engine
{
    public class EngineFacade : IEngine
    {
        ICache APIEngineCache { get; }
        string SupplierResourceName { get; }

        public EngineFacade()
        {
            APIEngineCache = new CacheFacade();
            (APIEngineCache as CacheFacade).SinglePostcodeServiceCallback = SinglePostcodeInformation;
            (APIEngineCache as CacheFacade).MultiPostcodeServiceCallback = MultiPostcodeInformation;

            SupplierResourceName = "postcodes";
        }

        public bool VerifyAPIAccessibility(string baseUrl)
        {
            try
            {
                System.Net.WebClient webClient = new System.Net.WebClient();
                string result = webClient.DownloadString(baseUrl);

                return true;
            }
            catch { return false; }
        }

        public IResponse GetSinglePostcodeInformation(string postcode, string supplierBaseUrl)
        {
            IResponse response = APIEngineCache.CacheSinglePostcodeRequest(postcode, supplierBaseUrl);
            return response;
        }

        public IResponse GetMultiPostcodesInformation(string postcodes, string supplierBaseUrl)
        {
            IResponse response = APIEngineCache.CacheMultiPostcodeRequest(postcodes, supplierBaseUrl);
            return response;
        }

        private IResponse SinglePostcodeInformation(string postcode, string supplierBaseUrl)
        {
            IResponse response = null;

            RestClient client = GetRestClient(supplierBaseUrl + SupplierResourceName + @"/" + postcode?.Trim());
            RestRequest request = new RestRequest();
            request.Method = Method.GET;

            SupplierResponse supplierResponse = JsonConvert.DeserializeObject<SupplierResponse>(client.Execute(request).Content);

            if (supplierResponse.status == (int)SupplierErrorCodes.Success)
            {
                response = MapToDTO(supplierResponse);
            }
            else
            {
                response = ErrorResponseFactory.GetErrorObject((SupplierErrorCodes)supplierResponse.status);
            }

            return response;
        }

        
        private IResponse MultiPostcodeInformation(string postcodes, string supplierBaseUrl)
        {
            IResponse response = null;

            RestClient client = GetRestClient(supplierBaseUrl + SupplierResourceName);
            RestRequest request = new RestRequest();
            request.Method = Method.POST;

            SupplierBulkRequest supplierRequest = new SupplierBulkRequest();
            supplierRequest.postcodes = postcodes?.Split(",").Select(s => s.Trim()).ToArray();

            request.AddJsonBody(supplierRequest);

            SupplierBulkResponse supplierResponse = JsonConvert.DeserializeObject<SupplierBulkResponse>(client.Execute(request).Content);

            if (supplierResponse.status == (int)SupplierErrorCodes.Success)
            {
                response = MapToDTO(supplierResponse);
            }
            else
            {
                response = ErrorResponseFactory.GetErrorObject((SupplierErrorCodes)supplierResponse.status);
            }

            return response;
        }

        private RestClient GetRestClient(string baseUrl)
        {
            RestClient client = new RestClient(baseUrl);
            client.Timeout = 60000; // 1 min Timeout
            client.ReadWriteTimeout = 60000; // 1 min Timeout

            return client;
        }

        private PostcodeDetails MapToDTO(SupplierResponse supplierResponse)
        {
            PostcodeDetails details = new PostcodeDetails();
            details.Postcode = supplierResponse.result.postcode;
            details.Coordinates = new Coordinates();
            details.Coordinates.Longitude = supplierResponse.result.longitude;
            details.Coordinates.Latitude = supplierResponse.result.latitude;

            return details;
        }

        private MultiPostCodeDetails MapToDTO(SupplierBulkResponse supplierBulkResponse)
        {
            MultiPostCodeDetails bulkDetails = new MultiPostCodeDetails();
            bulkDetails.PostcodeDetailsList = new List<IPostcodeDetails>();

            foreach (BulkResult bulkResult in supplierBulkResponse.result)
            {
                bulkDetails.PostcodeDetailsList.Add(ExtractPostcodeInformation(bulkResult));
            }

            return bulkDetails;
        }

        private IPostcodeDetails ExtractPostcodeInformation(BulkResult bulkResult)
        {
            IPostcodeDetails details = new PostcodeDetails();
            if (bulkResult.result != null)
            {
                details.Postcode = bulkResult.result.postcode;
                details.Coordinates = new Coordinates();
                details.Coordinates.Longitude = bulkResult.result.longitude;
                details.Coordinates.Latitude = bulkResult.result.latitude;
            }
            else
            {
                details.Postcode = bulkResult.query;
            }

            return details;
        }
    }
}
 