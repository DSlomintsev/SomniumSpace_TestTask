using System.Collections.Generic;
using UnityEngine;

namespace Common.Utils
{
    public static class SpawnUtils
    {
        private static List<GameObject> Items = new ();
        
        public static T Instantiate<T>(T gameObject, Transform container = null) where T : Component
        {
            var item = GameObject.Instantiate(gameObject,container);
            Items.Add(item.gameObject);
            return item;
        }

        public static void Destroy(GameObject gameObject)
        {
            if (gameObject != null)
            {
                var isResult = Items.Remove(gameObject);
  //              if (!isResult)
//                    Debug.Log("CAN'T REMOVE=" + gameObject.name);

                GameObject.Destroy(gameObject);
            }
        }

        public static void ShowItems()
        {
            Debug.Log("Show Items:");
            foreach (var item in Items)
                Debug.Log(item);
        }
    }
}