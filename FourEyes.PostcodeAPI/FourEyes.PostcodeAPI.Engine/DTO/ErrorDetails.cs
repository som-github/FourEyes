using FourEyes.PostcodeAPI.Interfaces;

namespace FourEyes.PostcodeAPI.Engine.DTO
{
    public class ErrorDetails : IError
    {
        public int Status { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
