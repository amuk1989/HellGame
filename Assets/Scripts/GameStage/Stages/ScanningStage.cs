﻿using GameStage.Interfaces;
using PlaneMeshing.Interfaces;
using Scanning.Interfaces;

namespace GameStage.Stages
{
    public class ScanningStage: IGameStage
    {
        private readonly IScanningService _scanningService;
        private readonly IPlaneRecognizer _planeRecognizer;

        public ScanningStage(IScanningService scanningService, IPlaneRecognizer planeRecognizer)
        {
            _scanningService = scanningService;
            _planeRecognizer = planeRecognizer;
        }

        public void Execute()
        {
            _planeRecognizer.StartRecognizer();
            _scanningService.AsyncScanningTask();
        }

        public void Complete()
        {
            _planeRecognizer.StopRecognizer();
        }
    }
}