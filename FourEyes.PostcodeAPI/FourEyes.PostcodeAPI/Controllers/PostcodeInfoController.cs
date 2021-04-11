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

        public PostcodeInfoController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("postcode-info/v1/get-single-postcode-info")]
        public IResponse GetSinglePostcodeInfo([FromServices] IEngine engine, string postcode)
        {
            return engine.GetSinglePostcodeInformation(postcode, Configuration[Constants.POSTCODE_IO_BASE_URL]);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("postcode-info/v1/get-multi-postcodes-info")]
        public IResponse GetMultiPostcodesInformation([FromServices] IEngine engine, string postcodes)
        {
            return engine.GetMultiPostcodesInformation(postcodes, Configuration[Constants.POSTCODE_IO_BASE_URL]);
        }
    }
}
