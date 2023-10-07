using AR.Aggregates;
using AR.Interfaces;

namespace AR.Services
{
    public class ARService: IARService
    {
        private readonly ARAggregate _aggregate;

        internal ARService(ARAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public virtual void Initialize()
        {
            _aggregate.CreateARComponents();
        }
    }
}