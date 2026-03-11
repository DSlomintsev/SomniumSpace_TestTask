using UnityEngine;

namespace Common.Utils
{
    public class MathUtils
    {
        public static bool IsPointInCircle(Vector2 point, Vector2 circleCenter, float radius)
        {
            return (point.x - circleCenter.x) * (point.x - circleCenter.x) + (point.y - circleCenter.y) * (point.y - circleCenter.y) <= radius * radius;
        }

        public static float GetDistance(Vector2 point1, Vector2 point2, float radius)
        {
            var dLat = ToRadians(point2.x - point1.x);
            var dLon = ToRadians(point2.y - point1.y);
            var a =
                Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                Mathf.Cos(ToRadians(point1.x)) * Mathf.Cos(ToRadians(point2.x)) *
                Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);

            var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
            var d = radius * c;
            return d;
        }

        public static float ToRadians(float deg)
        {
            return deg * (Mathf.PI / 180);
        }
    }
}