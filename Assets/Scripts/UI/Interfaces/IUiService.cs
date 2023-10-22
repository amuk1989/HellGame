using UnityEngine;
using Zenject;

namespace UI.Interfaces
{
    public interface IUiService: IInitializable
    {
        public void SetOnCanvas(RectTransform view);
    }
}