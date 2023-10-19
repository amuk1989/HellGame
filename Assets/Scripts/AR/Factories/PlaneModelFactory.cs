using AR.Data;
using AR.Models;
using Zenject;

namespace AR.Factories
{
    public class PlaneModelFactory: IFactory<PlaneData, PlaneModel>
    {
        private readonly DiContainer _diContainer;

        public PlaneModelFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public PlaneModel Create(PlaneData param)
        {
            return _diContainer.Instantiate<PlaneModel>(new object[]{param});
        }
    }
}