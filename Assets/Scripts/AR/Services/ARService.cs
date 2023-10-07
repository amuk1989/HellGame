using AR.Aggregates;
using AR.Interfaces;

namespace AR.Services
{
    public class ARService: IARService
    {
        private readonly ARAggregate _aggregate;

        private ARService(ARAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public void Initialize()
        {
            _aggregate.CreateARComponents();
        }
    }
}