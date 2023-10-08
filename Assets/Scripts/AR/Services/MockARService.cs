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
            PlaceholderFactory<MockRoom> mockRoomFactory) : base(aggregate, arPlaneRepository)
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