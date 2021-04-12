using FourEyes.PostcodeAPI.Engine;
using FourEyes.PostcodeAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FourEyes.PostcodeAPI.Controllers
{
    [ApiController]
    public class PostcodeInfoController : ControllerBase
    {
        IConfiguration Configuration { get; }

        IEngine APIEngine { get; }

        public PostcodeInfoController(IConfiguration configuration, IEngine engine)
        {
            Configuration = configuration;
            APIEngine = engine;
        }

        /// <summary>
        /// This GET API takes the postcode as input to provide the geo-coordinate details of the Location
        /// </summary>
        /// <param name="postcode">Postcode to fetch the details for e.g. EX1 1NT or RM82GF</param>
        /// <returns>Coordinate details object</returns>
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("postcode-info/v1/get-single-postcode-info")]
        public IResponse GetSinglePostcodeInfo(string postcode)
        {
            return APIEngine.GetSinglePostcodeInformation(postcode, Configuration[Constants.POSTCODE_IO_BASE_URL]);
        }

        /// <summary>
        /// This GET API takes the comma separated postcodes list to provide the geo-coordinate details of all input postcodes
        /// </summary>
        /// <param name="postcodes">Comma separated postcodes for which details are needed e.g. EX1 1NT, RM82GF</param>
        /// <returns>The list of Object for coordinates details</returns>
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("postcode-info/v1/get-multi-postcodes-info")]
        public IResponse GetMultiPostcodesInformation(string postcodes)
        {
            return APIEngine.GetMultiPostcodesInformation(postcodes, Configuration[Constants.POSTCODE_IO_BASE_URL]);
        }
    }
}
