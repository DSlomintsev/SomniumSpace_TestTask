using System.Collections.Generic;
using Common.UI.Dialogs.BaseDialog;

namespace Common.Services.Dialogs
{
    public static class DialogServiceUtils
    {
        public static DialogItem GetDialog<T>(this List<DialogItem> dialogs) where T : BaseDialogMediator
        {
            for (var i = 0; i < dialogs.Count; i++)
            {
                var dialogItem = dialogs[i];
                if (dialogItem.Mediator is T)
                    return dialogItem;
            }

            return null;
        }

        public static T GetDialogMediator<T>(this List<DialogItem> dialogs) where T : BaseDialogMediator
        {
            for (var i = 0; i < dialogs.Count; i++)
            {
                var dialogMVVM = dialogs[i];
                if (dialogMVVM.Mediator is T viewModel)
                    return viewModel;
            }

            return null;
        }

        public static DialogItem GetDialogByMediator(this List<DialogItem> dialogs, BaseDialogMediator viewModel)
        {
            for (var i = 0; i < dialogs.Count; i++)
            {
                var dialogItem = dialogs[i];
                if (dialogItem.Mediator == viewModel)
                    return dialogItem;
            }

            return null;
        }
    }
}