using FourEyes.PostcodeAPI.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FourEyes.PostcodeAPI.Engine.DTO
{
    [DataContract(Namespace = Constants.DATA_CONTRACT_NAMESPACE)]
    public class MultiPostCodeDetails : IResponse
    {
        public List<IPostcodeDetails> PostcodeDetailsList { get; set; }
    }
}
