using Common.Utils;
using UnityEngine;

namespace Common.Services.UIs
{
    public class UIService: BaseService
    {
        private UIContainer _uiContainer;
        public RectTransform DialogsContainer => _uiContainer.dialogsContainer;

        public UIService(UIContainer uiContainerPrefab)
        {
            _uiContainer = SpawnUtils.Instantiate(uiContainerPrefab);
            GameObject.DontDestroyOnLoad(_uiContainer);
        }
    }
}