using UnityEngine;

namespace Common.Utils
{
    public static class TransformExt
    {
        public static Transform GetChildByName(this Transform transform, string name)
        {
            Transform result = null;

            foreach (Transform child in transform)
            {
                if (child.name == name)
                {
                    result = child;
                }
            }

            return result;
        }

        public static Transform GetChildByNameRecursion(this Transform parent, string name)
        {
            if (parent == null) return null;
            var result = parent.Find(name);
            if (result != null)
                return result;
            foreach (Transform child in parent)
            {
                result = child.GetChildByNameRecursion(name);
                if (result != null)
                    return result;
            }

            return null;
        }

        public static Transform GetChildByPathRecursion(string path)
        {
            Transform result;
            if (path.Contains("/"))
            {
                var items = path.Split("/");
                var gameObject = GameObject.Find(items[0]);
                if (gameObject == null)
                    return null;
                
                result = gameObject.transform;
                for (var i = 1; i < items.Length; i++)
                    result = result.GetChildByNameRecursion(items[i]);
            }
            else
            {
                var gameObject = GameObject.Find(path);
                if (gameObject == null)
                    return null;
                
                result = gameObject.transform;   
            }

            return result;
        }
    }
}