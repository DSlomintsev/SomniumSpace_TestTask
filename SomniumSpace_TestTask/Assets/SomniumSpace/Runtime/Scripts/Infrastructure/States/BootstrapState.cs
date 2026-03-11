using CloserToTheSky.Infrastructure.States;
using Common.Infrastructure.States;
using Common.Services;
using Common.Services.Dialogs;
using Common.UI.Dialogs.LoadingDialog;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SomniumSpace.Infrastructure.States
{
    public class BootstrapState : IState
    {
        public void Enter() => EnterAsync().Forget();
        
        private async UniTask EnterAsync()
        {
            /*_gameModel = ModelLocator.Get<GameModel>();
            _appStateMachine = ServiceLocator.Get<AppStateMachine>();
            _dialogService = ServiceLocator.Get<DialogService>();

            CheckVersion();

            var loadingScreen = _dialogService.OpenDialog<LoadingScreenViewModel>(AudioId.NONE, config: DialogServiceUtils.StaticScreen);
            loadingScreen.SetVersion(Application.version);

            Random.InitState(0);*/
            
            var dialogService = ServiceLocator.Get<DialogService>();
            var loadingScreen = dialogService.OpenDialog<LoadingDialogMediator>(config: DialogService.DialogConfig);
            await UniTask.Delay(1000);
            loadingScreen.SetVersion(Application.version);
            dialogService.CloseDialog<LoadingDialogMediator>();
            HandleSceneLoaded();
        }

        private static void CheckVersion()
        {
            /*var appVersion = "AppVersion";
            var currentVersion = AppConfig.VERSION;
            var savedVersion = PlayerPrefs.GetString(appVersion, "");

            if (!savedVersion.Equals(currentVersion))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetString(appVersion, currentVersion);
                PlayerPrefs.Save();
            }*/
        }

        public void Exit()
        {
        }

        private void HandleSceneLoaded() => ServiceLocator.Get<AppStateMachine>().Enter<InitState>();
    }
}