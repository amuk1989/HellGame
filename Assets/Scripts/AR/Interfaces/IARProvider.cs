using System;
using System.Collections.Generic;
using AR.Data;

namespace AR.Interfaces
{
    public interface IARProvider
    {
        public IObservable<PlaneData> OnPlaneUpdated { get; }
        public IObservable<PlaneData> OnPlaneRemoved { get; }
        public IReadOnlyList<PlaneData> Planes { get; }
    }
}