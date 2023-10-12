using System;
using System.Collections.Generic;
using AR.Interfaces;
using AR.View;
using UniRx;
using Zenject;

namespace AR.Aggregates
{
    internal class ARDebugger: IInitializable, IDisposable
    {
        private readonly PlaceholderFactory<AnchorView> _anchorFactory;
        private readonly IARProvider _arProvider;

        private readonly Dictionary<string, AnchorView> _anchors = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public ARDebugger(PlaceholderFactory<AnchorView> anchorFactory, IARProvider arProvider)
        {
            _anchorFactory = anchorFactory;
            _arProvider = arProvider;
        }

        public void Initialize()
        {
            _arProvider
                .OnPlaneUpdated
                .Subscribe(plane =>
                {
                    if (!_anchors.TryGetValue(plane.ID, out var anchor))
                    {
                        anchor = _anchorFactory.Create();
                    }
                    
                    anchor.transform.SetPositionAndRotation(plane.Center, plane.Rotation);
                    anchor.SetSize(plane.Extends);
                    _anchors[plane.ID] = anchor;
                })
                .AddTo(_compositeDisposable);
            
            _arProvider
                .OnPlaneRemoved
                .Subscribe(plane =>
                {
                    if (!_anchors.ContainsKey(plane.ID)) return;
                    var view = _anchors[plane.ID];
                    view.Dispose();
                    _anchors.Remove(plane.ID);
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}