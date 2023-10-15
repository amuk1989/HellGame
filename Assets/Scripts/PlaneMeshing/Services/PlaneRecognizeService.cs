using AR.Interfaces;
using PlaneMeshing.Interfaces;
using UniRx;

namespace PlaneMeshing.Services
{
    public class PlaneRecognizeService: IPlaneRecognizer
    {
        private readonly IARProvider _arProvider;

        public PlaneRecognizeService(IARProvider arProvider)
        {
            _arProvider = arProvider;
        }

        public void StartRecognizer()
        {

        }

        public void StopRecognizer()
        {
            
        }
    }
}