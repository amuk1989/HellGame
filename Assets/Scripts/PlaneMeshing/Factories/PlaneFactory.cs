using AR.Data;
using PlaneMeshing.View;
using UnityEngine;
using Utility;
using Zenject;

namespace PlaneMeshing.Factories
{
    public class PlaneFactory: IFactory<LayerMask, Material, Mesh, PlaneView>
    {
        private readonly DiContainer _diContainer;

        public PlaneFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public PlaneView Create(LayerMask mask, Material material, Mesh param)
        {
            return _diContainer
                .InstantiatePrefabResourceForComponent<PlaneView>(Consts.Plane, new object[] {mask, material, param});
        }
    }
}