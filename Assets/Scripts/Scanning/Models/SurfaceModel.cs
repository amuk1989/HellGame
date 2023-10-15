using Base.Interfaces;
using Scanning.Data;

namespace Scanning.Models
{
    public class SurfaceModel: IModel
    {
        public string Id { get; private set;}
        public SurfaceType SurfaceType { get; private set; }
    }
}