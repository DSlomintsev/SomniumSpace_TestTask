using SomniumSpace.Models;
using Common.Models;
using Common.Services;
using Common.Services.Dialogs;
using Common.UI.Dialogs.BaseDialog;

namespace SomniumSpace.UI.Dialogs.GameUI
{
    public class GameUIMediator : BaseDialogMediator
    {
        private GameUIView _view;

        private PlayersModel _playersModel;
        private DialogService _dialogService;

        public override void Init(BaseDialogView view)
        {
            base.Init(view);
            _view = (GameUIView)view;

            _dialogService = ServiceLocator.Get<DialogService>();
            _playersModel = ModelsLocator.Get<PlayersModel>();
        }

        public override void DeInit()
        {
            base.DeInit();
        }
    }
}
