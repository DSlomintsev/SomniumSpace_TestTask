using UnityEngine;

namespace Common.Utils
{
    public static class FloatExt
    {
        public static float RoundTo(this float value, float round) => Mathf.RoundToInt(value / round) * round;
        public static float ToBigger(this float value, float round) => value < 0 ? Mathf.Min(-round, value) : Mathf.Max(round, value);
        public static float FloorToInt(this float value, float round) => Mathf.FloorToInt(value / round) * round;
        public static float CeilToInt(this float value, float round) => Mathf.CeilToInt(value / round) * round;
    }
}