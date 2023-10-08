using AR.Aggregates;
using AR.Interfaces;
using AR.Repositories;
using AR.View;
using Zenject;

namespace AR.Services
{
    internal class MockARService: ARService
    {
        private readonly PlaceholderFactory<MockRoom> _mockRoomFactory;

        private MockARService(ARAggregate aggregate, ARPlaneRepository arPlaneRepository, 
            ARMeshRepository meshRepository,PlaceholderFactory<MockRoom> mockRoomFactory) : 
            base(aggregate, arPlaneRepository, meshRepository)
        {
            _mockRoomFactory = mockRoomFactory;
        }

        public override void StartCollection()
        {
            base.StartCollection();
            _mockRoomFactory.Create();
        }
    }
}