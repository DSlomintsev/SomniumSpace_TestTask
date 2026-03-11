using System.Collections.Generic;
using UnityEngine;

namespace Common.Utils
{
    public static class ListExt
    {
        public static T RandomItem<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
        
        public static void RemoveUnordered<T>(this List<T> list, int index) where T: struct
        {
            int lastIndex = list.Count - 1;
            list[index] = list[lastIndex];
            list.RemoveAt(lastIndex);
        }
    }
}