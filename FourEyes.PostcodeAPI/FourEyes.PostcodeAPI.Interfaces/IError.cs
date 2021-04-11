namespace FourEyes.PostcodeAPI.Interfaces
{
    public interface IError : IResponse
    {
        int Status { get; set; }
        string Error { get; set; }
        string Message { get; set; }
    }
}
