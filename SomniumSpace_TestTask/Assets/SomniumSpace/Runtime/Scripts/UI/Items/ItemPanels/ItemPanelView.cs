using System;
using System.Collections.Generic;
using Common.UI.Dialogs.BaseDialog;
using Common.Utils;
using UnityEngine;

namespace SomniumSpace.UI.Items.ItemPanels
{
    public class ItemPanelView<T, F> : BaseDialogView
    {
        [SerializeField] private ItemPanel<T, F> itemPanel;

        public void SetTitle(string title) => itemPanel.SetTitle(title);
        public void SetItems(List<T> items) => itemPanel.SetItems(items);

        public void SetActions(Action<T> clickAction, Action closeAction)
        {
            base.SetAction(closeAction);

            itemPanel.SetAction(clickAction);
        }
    }
}