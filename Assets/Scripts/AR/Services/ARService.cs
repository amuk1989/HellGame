using AR.Interfaces;
using AR.View;
using Niantic.ARDK.AR;
using Zenject;

namespace AR.Services
{
    public class ARService: IARService
    {
        private readonly ARManager.ARFactory _arFactory;
        private ARManager _arManager;

        internal ARService(ARManager.ARFactory arFactory)
        {
            _arFactory = arFactory;
        }

        public void Initialize()
        {
            if (_arManager == null) _arManager = _arFactory.Create();
        }
    }
}