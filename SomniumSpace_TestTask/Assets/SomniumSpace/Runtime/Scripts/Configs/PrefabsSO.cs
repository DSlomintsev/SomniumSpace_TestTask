using SomniumSpace.Actors.Player;
using Common.Input;
using Common.Services.UIs;
using Fusion;
using SomniumSpace.Services.Networks;
using UnityEngine;

namespace SomniumSpace.Configs
{
    [CreateAssetMenu(fileName = "PrefabsSO", menuName = "ScriptableObjects/Prefabs", order = 1)]
    public class PrefabsSO:ScriptableObject
    {
        public UIContainer UIContainer;
        public InputReader InputReader;
        public PlayerGfx PlayerGfx;
        public NetworkPrefabRef Player;
        public VoiceComponent VoiceComponent;
        public Transform NetworkRunner;
    }
}