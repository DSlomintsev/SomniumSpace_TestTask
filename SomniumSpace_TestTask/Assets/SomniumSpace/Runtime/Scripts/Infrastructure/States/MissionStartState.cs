using SomniumSpace.UI.Dialogs.Missions.MissionStartDialog;
using Common.Infrastructure.States;
using Common.Services;
using Common.Services.Dialogs;
using Cysharp.Threading.Tasks;

namespace SomniumSpace.Infrastructure.States
{
    public class MissionStartState : IState
    {
        public void Enter() => EnterAsync().Forget();

        private async UniTask EnterAsync()
        {
            var dialogService = ServiceLocator.Get<DialogService>();
            var missionStartDialog = dialogService.OpenDialog<MissionStartDialogMediator>(config: DialogService.DialogConfig);
            await UniTask.Delay(1000);
            // load some async data, prepare pools
            dialogService.CloseDialog<MissionStartDialogMediator>();

            HandleSceneLoaded();
        }

        public void Exit()
        {
        }

        private void HandleSceneLoaded() => ServiceLocator.Get<AppStateMachine>().Enter<AppLoopState>();
    }
}