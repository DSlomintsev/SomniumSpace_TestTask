using System;
using SomniumSpace.Services.Localization;
using Common.Components.UI;
using Common.Services;
using Common.Utils;
using UnityEngine;

namespace SomniumSpace.UI.Items.ItemPanels
{
    public class ItemPanelCategoryUIBase<T> : MonoBehaviour
    {
        [SerializeField] private ExtTMPText titleLabel;
        [SerializeField] private ExtBtn actionBtn;

        private Action<T> _click;
        private T _id;

        public void SetData(T id)
        {
            _id = id;
        }

        public void SetAction(Action<T> click)
        {
            _click = click;

            UpdateView();
        }

        public void DeInit()
        {

        }

        private void UpdateView() => titleLabel.SetText(ServiceLocator.Get<LocalizationService>().GetLocale(_id.ToString()));

        public void OnClick() => _click.Call(_id);
    }
}