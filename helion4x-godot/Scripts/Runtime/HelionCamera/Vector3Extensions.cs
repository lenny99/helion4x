using Godot;

namespace Helion4x.Runtime.HelionCamera
{
    internal static class Vector3Extensions
    {
        public static Vector3 Lerp(this Vector3 from, Vector3 to, float weight)
        {
            var x = Mathf.Lerp(from.x, to.x, weight);
            var y = Mathf.Lerp(from.y, to.y, weight);
            var z = Mathf.Lerp(from.z, to.z, weight);
            return new Vector3(x, y, z);
        }
    }
}