using SomniumSpace.UI.Dialogs.Missions.MissionEndDialog;
using Common.Infrastructure.States;
using Common.Services;
using Common.Services.Dialogs;
using Cysharp.Threading.Tasks;

namespace SomniumSpace.Infrastructure.States
{
    public class MissionEndState : IState
    {
        public void Enter() => EnterAsync().Forget();
        
        public async UniTask EnterAsync()
        {
            var dialogService = ServiceLocator.Get<DialogService>();
            var missionEndDialog = dialogService.OpenDialog<MissionEndDialogMediator>(config: DialogService.DialogConfig);
            await UniTask.Delay(1000);
            // unload mission resources, clear pools
            dialogService.CloseDialog<MissionEndDialogMediator>();
            
            HandleSceneLoaded();
        }

        public void Exit()
        {
        }

        private void HandleSceneLoaded() => ServiceLocator.Get<AppStateMachine>().Enter<MissionStartState>();
    }
}