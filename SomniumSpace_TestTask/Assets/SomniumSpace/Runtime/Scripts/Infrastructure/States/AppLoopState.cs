using System.Threading;
using Common.Infrastructure.States;
using Common.Services;
using Common.Services.Dialogs;
using Cysharp.Threading.Tasks;
using Fusion;
using SomniumSpace.Runtime.Scripts.Services.Networks.UI.NetworkUI;
using SomniumSpace.Services.Networks;
using SomniumSpace.Services.Networks.MainMenuDialog;
using UnityEngine;

namespace SomniumSpace.Infrastructure.States
{
    public class AppLoopState : IState
    {
        public void Enter() => EnterAsync().Forget();

        private CancellationTokenSource _ctx;

        private async UniTask EnterAsync()
        {
            Debug.Log("DSiT EnterAsync");
            var dialogService = ServiceLocator.Get<DialogService>();
            
            _ctx?.Cancel();
            _ctx = new CancellationTokenSource();
            
            ServiceLocator.Get<NetworkService>().OnRunnerStarted += OnRunnerStarted;

            dialogService.OpenDialog<MainMenuDialogMediator>();

            StartFlow();
        }

        private void OnRunnerStarted(NetworkRunner obj)
        {
            ServiceLocator.Get<VoiceService>().Start();

            var dialogService = ServiceLocator.Get<DialogService>();
            dialogService.OpenDialog<NetworkUIMediator>();
        }

        private void StartFlow()
        {
            //SpawnPlayerCommand.Do(true);

            //ServiceLocator.Get<TickService>().AddTickIndexAction(1, Update);
        }

        public void Exit()
        {
        }

        private void HandleSceneLoaded()
        {
            ServiceLocator.Get<AppStateMachine>().Enter<MissionEndState>();
        }
    }
}