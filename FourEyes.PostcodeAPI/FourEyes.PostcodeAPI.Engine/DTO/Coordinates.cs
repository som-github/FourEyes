using FourEyes.PostcodeAPI.Interfaces;
using System.Runtime.Serialization;

namespace FourEyes.PostcodeAPI.Engine.DTO
{
    [DataContract(Namespace = Constants.DATA_CONTRACT_NAMESPACE)]
    public class Coordinates : ICoordinates
    {
        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string Latitude { get; set; }
    }
}
