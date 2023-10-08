using AR.Data;
using Niantic.ARDK.AR.Anchors;
using Niantic.ARDK.AR.ARSessionEventArgs;

namespace AR.Utilities
{
    public class PlanesUtility
    {
        internal static PlaneData AddPlane(AnchorsArgs args)
        {
            foreach (var arAnchor in args.Anchors)
            {
                var anchor = arAnchor as IARPlaneAnchor;
                if (anchor == null) continue;
                
            }

            return new PlaneData();
        }
        
        internal static PlaneData MergePlanes(AnchorsMergedArgs args)
        {
            foreach (var arAnchor in args.Children)
            {
                var anchor = arAnchor as IARPlaneAnchor;
                if (anchor == null) continue;
                
            }
            
            return new PlaneData();
        }
        
        internal PlaneData RemovePlane(AnchorsArgs args)
        {
            foreach (var arAnchor in args.Anchors)
            {
                var anchor = arAnchor as IARPlaneAnchor;
                if (anchor == null) continue;
                
            }
            
            return new PlaneData();
        }
        
        internal PlaneData UpdatePlane(AnchorsArgs args)
        {
            foreach (var arAnchor in args.Anchors)
            {
                var anchor = arAnchor as IARPlaneAnchor;
                if (anchor == null) continue;
                
            }
            
            return new PlaneData();
        }
    }
}