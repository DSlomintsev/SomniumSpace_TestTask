using SomniumSpace.Configs.UI;
using UnityEngine;

namespace SomniumSpace.Configs.Entities
{
    [CreateAssetMenu(fileName = "EntitySO", menuName = "ScriptableObjects/EntitySO", order = 1)]
    public class EntitySO:ScriptableObject
    {
        //public TransformComponent Pos;
        //public Vector3 Rot;
        public Vector3 Scale;
        public DialogsSO Dialogs;
        public IconsSO Icons;
    }
}