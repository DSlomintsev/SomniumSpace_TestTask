using SomniumSpace.Actors.Entities.Systems;
using SomniumSpace.Configs;
using SomniumSpace.Infrastructure.States;
using SomniumSpace.Models;
using SomniumSpace.Services.Localization;
using SomniumSpace.Services.Resource;
using SomniumSpace.UI.Dialogs.GameUI;
using Common.Input;
using Common.Models;
using Common.Services;
using Common.Services.Dialogs;
using Common.Services.Tick;
using Common.Services.UIs;
using Cysharp.Threading.Tasks;
using SomniumSpace.Services.Networks;
using UnityEngine;

namespace SomniumSpace.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ConfigsSO configsSO;

        private void Construct()
        {
            ModelsLocator.Add(new GameModel(configsSO));
            ModelsLocator.Add(new PlayersModel());

            ServiceLocator.Add(new AppStateMachine());
            ServiceLocator.Add(new TickService());
            ServiceLocator.Add(new InputService(configsSO.Prefabs.InputReader));
            ServiceLocator.Add(new ResourceService(configsSO.Icons));
            ServiceLocator.Add(new LocalizationService());
            ServiceLocator.Add(new UIService(configsSO.Prefabs.UIContainer));
            ServiceLocator.Add(new DialogService(configsSO.Dialogs.ModalBkg, configsSO.Dialogs.Items));
            ServiceLocator.Add(new NetworkService());
            ServiceLocator.Add(new VoiceService());
            ServiceLocator.Init();
            
            // used for entities
            SystemLocator.Init();

            ServiceLocator.Get<DialogService>().OpenDialog<GameUIMediator>(config: DialogService.DialogConfig);
        }

        private void Awake() => EnterAsync().Forget();

        private async UniTask EnterAsync()
        {
            Construct();
            await UniTask.NextFrame();
            ServiceLocator.Get<AppStateMachine>().Enter<BootstrapState>();
        }
    }
}
