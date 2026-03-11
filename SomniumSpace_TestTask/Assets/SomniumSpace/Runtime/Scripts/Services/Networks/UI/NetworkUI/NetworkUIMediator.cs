using System.Threading;
using Common.Models;
using Common.Services;
using Common.UI.Dialogs.BaseDialog;
using Cysharp.Threading.Tasks;
using Fusion;
using SomniumSpace.Configs;
using SomniumSpace.Models;
using SomniumSpace.Services.Networks;
using UnityEngine;

namespace SomniumSpace.Runtime.Scripts.Services.Networks.UI.NetworkUI
{
    public class NetworkUIMediator : BaseDialogMediator
    {
        private NetworkUIView _view;

        private NetworkService _networkService;
        private VoiceService _voiceService;
        private NetworkConfigSO _networkConfig;
        private CancellationTokenSource _updateViewCtx;

        public override void Init(BaseDialogView view)
        {
            base.Init(view);
            _view = (NetworkUIView)view;

            _view.Construct(OnMutePressed);

            _networkConfig = ModelsLocator.Get<GameModel>().Configs.Network;

            _networkService = ServiceLocator.Get<NetworkService>();
            _voiceService = ServiceLocator.Get<VoiceService>();

            SubscribeToNetworkEvents();
            _view.SetStatus("Disconnected");
            _view.SetPlayerCount(0, _networkConfig.MaxPlayers);

            _view.RefreshMuteLabel(_voiceService.IsMuted);
        }

        public override void DeInit()
        {
            base.DeInit();

            UnsubscribeFromNetworkEvents();
            _updateViewCtx?.Cancel();
        }

        private void SubscribeToNetworkEvents()
        {
            _networkService.OnRunnerStarted += HandleRunnerStarted;
            _networkService.OnRunnerShutdown += HandleRunnerShutdown;
            _networkService.OnRemotePlayerJoined += _ => RefreshPlayerCount();
            _networkService.OnRemotePlayerLeft += _ => RefreshPlayerCount();
            _networkService.OnConnectionError += HandleError;
        }

        private void UnsubscribeFromNetworkEvents()
        {
            _networkService.OnRunnerStarted -= HandleRunnerStarted;
            _networkService.OnRunnerShutdown -= HandleRunnerShutdown;
            _networkService.OnRemotePlayerJoined -= _ => RefreshPlayerCount();
            _networkService.OnRemotePlayerLeft -= _ => RefreshPlayerCount();
            _networkService.OnConnectionError -= HandleError;
        }

        private void HandleRunnerStarted(NetworkRunner runner)
        {
            _view.SetStatus("Connected");
            RefreshPlayerCount();
            //start ctx
            _updateViewCtx?.Cancel();
            _updateViewCtx = new CancellationTokenSource();
            UpdateViewAsync(_updateViewCtx.Token);
        }

        private void HandleRunnerShutdown()
        {
            _view.SetStatus("Disconnected");
            _view.SetPlayerCount(0, _networkConfig.MaxPlayers);
            _view.SetPing(0);

            _updateViewCtx?.Cancel();
        }

        private void HandleError(string reason) => _view.SetStatus($"Error: {reason}");

        private async UniTask UpdateViewAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                RefreshPlayerCount();
                RefreshPing();
                await UniTask.Delay((int)(_networkConfig.UIRefreshInterval * 1000), cancellationToken: cancellationToken);
            }
        }

        private void RefreshPlayerCount()
        {
            if (_networkService?.Runner == null) return;
            _view.SetPlayerCount(_networkService.Runner.ActivePlayers.Count(), _networkConfig.MaxPlayers);
        }

        private void RefreshPing()
        {
            if (_networkService?.Runner == null) return;
            _view.SetPing(_networkService.Runner.GetPlayerRtt(_networkService.Runner.LocalPlayer));
        }

        private void OnMutePressed()
        {
            _voiceService.SetMuted(!_voiceService.IsMuted);
            _view.RefreshMuteLabel(_voiceService.IsMuted);
        }
    }
}
