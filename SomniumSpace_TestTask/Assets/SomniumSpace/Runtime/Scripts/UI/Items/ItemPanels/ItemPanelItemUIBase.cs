using System;
using Common.Components.UI;
using Common.Utils;
using UnityEngine;

namespace SomniumSpace.UI.Items.ItemPanels
{
    public class ItemPanelItemUIBase<T>:MonoBehaviour
    {
        [SerializeField] protected ExtTMPText titleLabel;
        [SerializeField] protected ExtTMPText descriptionLabel;
        [SerializeField] protected ExtImage icon;
        [SerializeField] protected ExtBtn actionBtn;

        private Action<T> _click;
        protected T _itemData;

        public void SetAction(Action<T> click) => _click = click;
        public void OnClick() => _click.Call(_itemData);

        public void SetData(T itemSO)
        {
            _itemData = itemSO;

            UpdateView();
        }

        protected virtual void UpdateView()
        {
            throw new NotImplementedException();
        }

        public void EnableAction() => actionBtn.interactable = true;
        public void DisableAction() => actionBtn.interactable = false;

        public void DeInit() => actionBtn.DeInit();
    }
}