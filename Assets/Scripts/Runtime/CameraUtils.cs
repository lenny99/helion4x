using UnityEngine;

namespace Helion4x.Runtime
{
    public static class CameraUtils
    {
        public static float DistanceFromCamera(Vector3 position)
        {
            var cameraPosition = Camera.main!.transform.position;
            return Vector3.Distance(position, cameraPosition);
        }
    }
}