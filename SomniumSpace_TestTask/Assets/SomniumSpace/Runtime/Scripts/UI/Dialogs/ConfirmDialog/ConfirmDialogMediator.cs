using Common.UI.Dialogs.BaseDialog;

namespace SomniumSpace.UI.Dialogs.ConfirmDialog
{
    public class ConfirmDialogMediator : BaseDialogMediator
    {
        private ConfirmDialogView _view;

        public override void Init(BaseDialogView view)
        {
            base.Init(view);

            _view = (ConfirmDialogView)view;
            _view.SetActions(Confirm, CloseDialog);
        }

        private void Confirm()
        {
        }

        public override void DeInit()
        {
            base.DeInit();
        }

        public void SetData(string title, string description)
        {
            _view.SetTitle(title);   
            _view.SetDescription(description);   
        }
    }
}