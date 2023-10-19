using AR.Data;
using AR.Models;
using AR.View;
using UI.Interfaces;
using UnityEngine;
using Zenject;

namespace AR.Factories
{
    public class AnchorDebugInfoFactory: IFactory<PlaneModel, AnchorDebugInfo>
    {
        private readonly DiContainer _diContainer;
        private readonly ARDebugConfig _debugConfig;
        private readonly IUiService _uiService;

        public AnchorDebugInfoFactory(DiContainer diContainer, ARDebugConfig debugConfig, IUiService uiService)
        {
            _diContainer = diContainer;
            _debugConfig = debugConfig;
            _uiService = uiService;
        }

        public AnchorDebugInfo Create(PlaneModel param)
        {
            var viewInfo = _diContainer
                .InstantiatePrefabForComponent<AnchorDebugInfo>(_debugConfig.DebugInfo, new object[] {param});
            
            _uiService.SetOnCanvas(viewInfo.transform as RectTransform);
            return viewInfo;
        }
    }
}