using AR.Aggregates;
using AR.Interfaces;
using AR.View;
using Zenject;

namespace AR.Services
{
    internal class MockARService: ARService
    {
        private readonly PlaceholderFactory<MockRoom> _mockRoomFactory;

        private MockARService(ARAggregate aggregate, PlaceholderFactory<MockRoom> mockRoomFactory) : base(aggregate)
        {
            _mockRoomFactory = mockRoomFactory;
        }

        public override void Initialize()
        {
            base.Initialize();
            _mockRoomFactory.Create();
        }
    }
}