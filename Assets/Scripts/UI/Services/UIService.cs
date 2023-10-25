using UI.Interfaces;
using UI.Views;
using UnityEngine;

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