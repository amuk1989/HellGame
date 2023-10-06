using AR.Interfaces;
using Niantic.ARDK.AR;
using Zenject;

namespace AR.Services
{
    public class ARService: IARService
    {
        public void Initialize()
        {
            var session = ARSessionFactory.Create();
        }
    }
}