using FourEyes.PostcodeAPI.Interfaces;
using System.Runtime.Serialization;

namespace FourEyes.PostcodeAPI.Engine.DTO
{
    [DataContract(Namespace = Constants.DATA_CONTRACT_NAMESPACE)]
    public class PostcodeDetails : IPostcodeDetails
    {
        [DataMember]
        public string Postcode { get; set; }

        [DataMember]
        public ICoordinates Coordinates { get; set; }
    }
}
