using SomniumSpace.Configs.UI;
using UnityEngine;

namespace SomniumSpace.Configs
{
    [CreateAssetMenu(fileName = "ConfigsSO", menuName = "ScriptableObjects/Configs", order = 1)]
    public class ConfigsSO:ScriptableObject
    {
        public NetworkConfigSO Network;
        public PrefabsSO Prefabs;
        public DialogsSO Dialogs;
        public IconsSO Icons;
    }
}