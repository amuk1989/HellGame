using System;
using System.Collections.Generic;
using AR.Data;
using AR.Interfaces;
using AR.View;
using UniRx;
using Zenject;

namespace AR.Controllers
{
    internal class ARDebugger: IInitializable, IDisposable
    {
        private readonly AnchorView.Factory _anchorFactory;
        private readonly AnchorDebugInfo.Factory _anchorInfoFactory;
        private readonly IARProvider _arProvider;
        private readonly ARDebugConfig _arDebugConfig;

        private readonly Dictionary<string, AnchorView> _anchors = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public ARDebugger(AnchorView.Factory anchorFactory, IARProvider arProvider, ARDebugConfig arDebugConfig,
            AnchorDebugInfo.Factory anchorInfoFactory)
        {
            _anchorFactory = anchorFactory;
            _arProvider = arProvider;
            _arDebugConfig = arDebugConfig;
            _anchorInfoFactory = anchorInfoFactory;
        }

        public void Initialize()
        {
            if (_arDebugConfig.IsShownAnchorPlanes) SubscribeToAnchorsUpdating();
        }

        private void SubscribeToAnchorsUpdating()
        {
            _arProvider
                .OnPlaneUpdated
                .Subscribe(plane =>
                {
                    if (_anchors.ContainsKey(plane.PlaneData.ID)) return;
                    
                    _anchors[plane.PlaneData.ID] = _anchorFactory.Create(plane);
                    
                    if (_arDebugConfig.IsShownAnchorInfo) _anchorInfoFactory.Create(plane);
                })
                .AddTo(_compositeDisposable);
            
            _arProvider
                .OnPlaneRemoved
                .Subscribe(plane =>
                {
                    if (!_anchors.ContainsKey(plane.PlaneData.ID)) return;
                    _anchors.Remove(plane.PlaneData.ID);
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}