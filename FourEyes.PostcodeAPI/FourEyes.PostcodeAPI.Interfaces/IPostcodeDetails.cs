namespace FourEyes.PostcodeAPI.Interfaces
{
    public interface IPostcodeDetails : IResponse
    {
        string Postcode { get; set; }

        ICoordinates Coordinates { get; set; }
    }
}
