using Common.Models;
using Common.Services;
using Common.Services.Dialogs;
using Common.UI.Dialogs.BaseDialog;
using Common.Utils;
using Cysharp.Threading.Tasks;
using Fusion;
using SomniumSpace.Configs;
using SomniumSpace.Models;

namespace SomniumSpace.Services.Networks.MainMenuDialog
{
    public class MainMenuDialogMediator : BaseDialogMediator
    {
        private MainMenuDialogView _view;

        private NetworkConfigSO _networkConfig;
        private NetworkService _networkService;

        public override void Init(BaseDialogView view)
        {
            base.Init(view);
            
            _networkConfig = ModelsLocator.Get<GameModel>().Configs.Network;
            _networkService = ServiceLocator.Get<NetworkService>();

            _view = (MainMenuDialogView)view;
            _view.Construct(OnJoinPressed, OnLeavePressed);
            _view.SetRoomId(_networkConfig.DefaultRoomName);

            SubscribeToNetworkEvents();
        }

        public override void DeInit()
        {
            base.DeInit();

            UnsubscribeFromNetworkEvents();
        }

        private void SubscribeToNetworkEvents()
        {
            _networkService.OnRunnerStarted  += OnNetworkServiceRunnerStarted;
            _networkService.OnRunnerShutdown += OnNetworkServiceOnRunnerShutdown;
            _networkService.OnConnectionError += ShowError;
        }

        private void UnsubscribeFromNetworkEvents()
        {
            _networkService.OnRunnerStarted  -= OnNetworkServiceRunnerStarted;
            _networkService.OnRunnerShutdown -= OnNetworkServiceOnRunnerShutdown;
            _networkService.OnConnectionError -= ShowError;
        }
        
        private void OnNetworkServiceOnRunnerShutdown() => UpdateView();
        private void OnNetworkServiceRunnerStarted(NetworkRunner networkRunner) => UpdateView();
        
        private void UpdateView()
        {
            _view.SetIsOnline(_networkService.IsOnline);
            _view.UpdateView();
        }

        private void ShowError(string errorMessage)
        {
            _view.SetIsOnline(_networkService.IsOnline);
            _view.ShowError(errorMessage);
        }

        private void OnJoinPressed(string roomId)
        {
            ServiceLocator.Get<DialogService>().CloseDialog(this);
            OnJoinPressedAsync(roomId).Forget();   
        }
        
        private async UniTask OnJoinPressedAsync(string roomId)
        {
            _view.SetFeedback("Connecting…");
            _view.SetInteractable(false);

            string roomName = roomId.IsNullOrEmpty() ? _networkConfig.DefaultRoomName : roomId.Trim();

            await _networkService.JoinOrCreateRoom(roomName);
        }

        private void OnLeavePressed() => OnLeavePressedAsync().Forget();
        
        private async UniTask OnLeavePressedAsync()
        {
            _view.SetFeedback("Disconnecting…");
            _view.SetInteractable(false);
            await _networkService.LeaveRoom();
        }
    }
}