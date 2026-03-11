using SomniumSpace.Infrastructure;
using SomniumSpace.Infrastructure.States;
using Common.Infrastructure.States;
using Common.Services;

namespace CloserToTheSky.Infrastructure.States
{
    public class InitState : IState
    {
        public void Enter()
        {
            /*_gameModel = ModelLocator.Get<GameModel>();
            _appStateMachine = ServiceLocator.Get<AppStateMachine>();
            _dialogService = ServiceLocator.Get<DialogService>();

            CheckVersion();
            
            var loadingScreen = _dialogService.OpenDialog<LoadingScreenViewModel>(AudioId.NONE, config: DialogServiceUtils.StaticScreen);
            loadingScreen.SetVersion(Application.version);
            
            Random.InitState(0);*/
            HandleSceneLoaded();
        }

        public void Exit()
        {
        }

        private void HandleSceneLoaded() => ServiceLocator.Get<AppStateMachine>().Enter<MissionStartState>();
    }
}