using System;
using System.Collections.Generic;
using Common.Components.UI;
using Common.Services.UIs;
using Common.UI.Dialogs.BaseDialog;
using Common.Utils;
using UnityEngine;

namespace Common.Services.Dialogs
{
    public class DialogService : BaseService
    {
        private ExtBtn _modalBkg;
        private List<BaseDialogView> _dialogPrefabs;
        
        private UIService _uiService;

        public event Action<DialogItem> DialogOpenedEvent;
        public event Action<DialogItem> DialogClosedEvent;
        public event Action DialogsUpdatedEvent;
        
        private const string MEDIATOR_POSTFIX = "Mediator";

        private readonly List<DialogItem> _dialogs = new();
        public List<DialogItem> Dialogs => _dialogs;
        
        public DialogService(ExtBtn modalBkg, List<BaseDialogView> dialogPrefabs)
        {
            _modalBkg = modalBkg;
            _dialogPrefabs = dialogPrefabs;
        }

        private BaseDialogView GetPrefabByName(string prefabName)
        {
            foreach (var prefab in _dialogPrefabs)
                if(prefab.name == prefabName)
                    return prefab;

            return null;
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            _uiService = ServiceLocator.Get<UIService>();
        }

        public T OpenDialog<T>(Transform parentContainer = null, bool isNew = false, BaseDialogConfig config = default) where T : BaseDialogMediator, new()
        {

            //_uiController.DisableUIInput();
            var dialogItem = GetDialog<T>();
            if (dialogItem == null || isNew)
            {
                dialogItem = CreateDialog<T>(parentContainer, config);

                _dialogs.Add(dialogItem);
            }

            if (dialogItem.View.State != DialogState.OPENED)
            {
                dialogItem.View.Show();

                DialogOpenedEvent.Call(dialogItem);
                DialogsUpdatedEvent.Call();
            }

            //_uiController.EnableUIInput();
            return (T)dialogItem.Mediator;
        }

        private DialogItem CreateDialog<T>(Transform parentContainer, BaseDialogConfig config) where T : BaseDialogMediator, new()
        {
            var viewName = typeof(T).Name.Replace(MEDIATOR_POSTFIX, string.Empty);
            parentContainer = parentContainer != null ? parentContainer : _uiService.DialogsContainer;

            var view = SpawnUtils.Instantiate<BaseDialogView>(GetPrefabByName(viewName), parentContainer);
            view.transform.SetAsLastSibling();

            if (config.IsModal)
            {
                var modalBkg = SpawnUtils.Instantiate(_modalBkg, view.transform);
                modalBkg.transform.SetAsFirstSibling();
                view.ModalBkg = modalBkg;    
            }

            var mediator = new T();
            mediator.Init(view);

            view.Init(config);
            view.Mediator = mediator;
            
            return new DialogItem {View = view, Mediator = mediator };
        }

        public void CloseDialog<T>() where T : BaseDialogMediator, new()
        {
            var dialogItem = GetDialog<T>();
            if(dialogItem == null) return;

            CloseDialog(dialogItem);
        }
        public void CloseDialog(BaseDialogMediator dialogMediator) => CloseDialog(GetDialogByMediator(dialogMediator));

        public void CloseDialog(DialogItem dialogItem)
        {
            var view = dialogItem.View;
            var mediator = dialogItem.Mediator;

            view.Hide();
            mediator.DeInit();
            OnViewHidden(dialogItem);

            _dialogs.Remove(dialogItem);

            DialogClosedEvent.Call(dialogItem);
            DialogsUpdatedEvent.Call();
        }

        public DialogItem GetDialog<T>() where T : BaseDialogMediator => _dialogs.GetDialog<T>();
        public DialogItem GetDialogByMediator(BaseDialogMediator mediator) => _dialogs.GetDialogByMediator(mediator);

        public DialogItem GetTopDialog() => _dialogs[^1];

        public bool IsDialogOpened<T>() where T : BaseDialogMediator
        {
            var dialog = GetDialog<T>();
            return dialog != null && dialog.View.State == DialogState.OPENED;
        }

        private void OnViewHidden(DialogItem dialogItem)
        {
            var view = dialogItem.View;
            view.Mediator = null;
            view.DeInit();
            SpawnUtils.Destroy(view.gameObject);
        }

        public static BaseDialogConfig DialogConfig = new BaseDialogConfig { IsModal = false };
        public static BaseDialogConfig ModalDialogConfig = new BaseDialogConfig { IsModal = true };
    }

    public class DialogItem
    {
        public BaseDialogView View;
        public BaseDialogMediator Mediator;
    }
    
    public struct BaseDialogConfig
    {
        public bool IsModal { get; set; }
    }
    
    
}