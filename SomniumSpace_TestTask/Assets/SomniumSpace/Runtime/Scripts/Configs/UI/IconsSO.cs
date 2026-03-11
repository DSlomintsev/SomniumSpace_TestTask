using UnityEngine;
using UnityEngine.U2D;

namespace SomniumSpace.Configs.UI
{
    [CreateAssetMenu(fileName = "IconsSO", menuName = "ScriptableObjects/UI/Icons", order = 1)]
    public class IconsSO:ScriptableObject
    {
        public SpriteAtlas Items;
    }
}