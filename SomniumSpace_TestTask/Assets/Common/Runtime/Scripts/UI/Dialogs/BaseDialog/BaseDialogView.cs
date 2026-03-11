using System;
using Common.Components.UI;
using Common.Services.Dialogs;
using Common.Utils;
using UnityEngine;

namespace Common.UI.Dialogs.BaseDialog
{
    public class BaseDialogView : MonoBehaviour
    {
        private Action _closeAction;

        public DialogState State;

        private BaseDialogConfig _config;
        public BaseDialogConfig Config => _config;

        public virtual BaseDialogMediator Mediator { get; set; }

        private ExtBtn _modalBkg;
        public ExtBtn ModalBkg
        {
            set
            {
                _modalBkg = value;
                _modalBkg.onClick.AddListener(Close);
            }
        }

        public virtual void Init(BaseDialogConfig config)
        {
            _config = config;
        }

        public virtual void DeInit()
        {
        }

        public void SetAction(Action closeAction) => _closeAction = closeAction;
        public void Show() => State = DialogState.OPENED;
        public void Hide() => State = DialogState.CLOSED;
        public virtual void Close() => _closeAction.Call();
    }

    public enum DialogState
    {
        NONE,
        OPENED,
        CLOSED
    }
}