using UnityEngine;

namespace Common.Utils
{
    public static class GameObjectExt
    {
        public static void ChangeLayer(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform)
                child.gameObject.ChangeLayer(layer);
        }
        
        public static bool IsParentGameObject(this Transform child, Transform parent) => child != null && (child == parent || IsParentGameObject(child.parent, parent));
    }
}