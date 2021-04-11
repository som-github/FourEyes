using FourEyes.PostcodeAPI.Engine;
using FourEyes.PostcodeAPI.Interfaces;

namespace FourEyes.PostcodeAPI
{
    public class PostcodeAPIFactory
    {
        public static IEngine GetEngineInstance()
        {
            return new EngineFacade();
        }
    }
}
