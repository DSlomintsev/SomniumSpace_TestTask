using Common.UI.Dialogs.BaseDialog;

namespace Common.UI.Dialogs.LoadingDialog
{
    public class LoadingDialogMediator: BaseDialogMediator
    {
        private LoadingDialogView _view;
        public override void Init(BaseDialogView view)
        {
            base.Init(view);
            _view= (LoadingDialogView)view;
        }
        
        public override void DeInit()
        {
            base.DeInit();
        }

        public void SetVersion(string version)=>_view.SetVersion(version);
    }
}