using UnityEngine;

namespace Common.Utils
{
    public static class MeshUtils
    {
        public static float GetHeight(this Mesh mesh)
        {
            foreach (var vertex in mesh.vertices)
            {
                Debug.Log(vertex.ToString());
            }

            return 0;
        }
    }
}