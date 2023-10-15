using System;
using System.Collections.Generic;
using Base.Interfaces;

namespace Scanning.Models
{
    public class RoomModel: IModel, IDisposable
    {
        public string Id { get; private set; }

        private List<SurfaceModel> _surfaces = new();

        public void Dispose()
        {
        }
    }
}