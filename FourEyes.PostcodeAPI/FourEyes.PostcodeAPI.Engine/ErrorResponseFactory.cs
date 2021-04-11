using FourEyes.PostcodeAPI.Engine.DTO;
using FourEyes.PostcodeAPI.Interfaces;
using static FourEyes.PostcodeAPI.Engine.Constants;

namespace FourEyes.PostcodeAPI.Engine
{
    public class ErrorResponseFactory
    {
        public static IError GetErrorObject(SupplierErrorCodes errorCode)
        {
            IError errorDetails = new ErrorDetails();

            switch(errorCode)
            {
                case SupplierErrorCodes.Invalid:
                    errorDetails.Status = 404;
                    errorDetails.Error = "NotFound";
                    errorDetails.Message = "You've entered an invalid postcode";
                    break;
                case SupplierErrorCodes.Blank:
                    errorDetails.Status = 422;
                    errorDetails.Error = "UnprocessableEntity";
                    errorDetails.Message = "The postcode is mandatory";
                    break;
                default:
                    errorDetails = null;
                    break;
            }

            return errorDetails;
        }
    }
}
