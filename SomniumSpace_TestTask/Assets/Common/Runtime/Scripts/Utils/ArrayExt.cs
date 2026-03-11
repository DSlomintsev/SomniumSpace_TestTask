using UnityEngine;


namespace Common.Utils
{
    public static class ArrayExt
    {
        public static T RandomItem<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
    }
}