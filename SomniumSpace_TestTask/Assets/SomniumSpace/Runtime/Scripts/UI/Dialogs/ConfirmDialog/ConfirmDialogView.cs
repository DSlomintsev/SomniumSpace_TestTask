using System;
using Common.Components.UI;
using Common.UI.Dialogs.BaseDialog;
using Common.Utils;
using UnityEngine;

namespace SomniumSpace.UI.Dialogs.ConfirmDialog
{
    public class ConfirmDialogView : BaseDialogView
    {
        [SerializeField] private ExtTMPText titleLabel;
        [SerializeField] private ExtTMPText descriptionLabel;
        
        private Action _confirm;

        public void SetTitle(string title) => titleLabel.SetText(title);
        public void SetDescription(string title) => descriptionLabel.SetText(title);

        public void SetActions(Action confirm, Action cancel)
        {
            base.SetAction(cancel);

            _confirm=confirm;
        }

        public void OnConfirm()=>_confirm.Call();
    }
}