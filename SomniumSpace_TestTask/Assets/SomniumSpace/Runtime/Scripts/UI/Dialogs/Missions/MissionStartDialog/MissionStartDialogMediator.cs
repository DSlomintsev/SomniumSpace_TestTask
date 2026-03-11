using Common.UI.Dialogs.BaseDialog;

namespace SomniumSpace.UI.Dialogs.Missions.MissionStartDialog
{
    public class MissionStartDialogMediator : BaseDialogMediator
    {
        private MissionStartDialogView _view;

        public override void Init(BaseDialogView view)
        {
            base.Init(view);

            _view = (MissionStartDialogView)view;
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