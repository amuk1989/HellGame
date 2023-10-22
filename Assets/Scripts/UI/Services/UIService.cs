using GameStage.Data;
using GameStage.Interfaces;
using UI.Interfaces;
using UI.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Services
{
    public class UIService: IUiService
    {
        private readonly UIComponent _uiComponent;
        
        private UIService(UIComponent uiComponent)
        {
            _uiComponent = uiComponent;
        }

        public void Initialize()
        {
        }

        public void SetOnCanvas(RectTransform view)
        {
            view.SetParent(_uiComponent.Transform);
        }
    }
}