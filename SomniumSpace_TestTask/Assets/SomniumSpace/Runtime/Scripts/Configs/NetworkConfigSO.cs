using UnityEngine;

namespace SomniumSpace.Configs
{
    [CreateAssetMenu(fileName = "NetworkConfigSO", menuName = "ScriptableObjects/NetworkConfig", order = 1)]
    public class NetworkConfigSO : ScriptableObject
    {
        // Fusion
        public int MaxPlayers = 4;
        public int MinPlayers = 2;
        public string DefaultRoomName = "DefaultRoom";
        public string GameSceneName = "GameScene";
        public string MenuSceneName = "MenuScene";

        // Interpolation
        /// <summary>How quickly position catches up to the network value (units/s²).</summary>
        public float PositionInterpolationSpeed = 15f;

        /// <summary>How quickly rotation catches up to the network value (deg/s²).</summary>
        public float RotationInterpolationSpeed = 20f;

        // Movement
        public float MoveSpeed = 5f;
        public float RotationSpeed = 180f;

        // Photon Voice
        public bool VoiceDebugEcho = false;
        public float VoiceAmplify = 1f;

        // UI refresh
        public float UIRefreshInterval = 0.5f;
    }
}
