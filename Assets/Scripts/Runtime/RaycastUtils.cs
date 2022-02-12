using UnityEngine;
using UnityEngine.InputSystem;

namespace Helion4x.Runtime
{
    public static class RaycastUtils
    {
        public static Vector3 GetPlanePosition()
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var mouseWorldPosition = Camera.main!.ScreenToWorldPoint(mousePosition);
            var ray = Camera.main!.ScreenPointToRay(mouseWorldPosition);
            return plane.Raycast(ray, out var entry) ? ray.GetPoint(entry) : Vector3.zero;
        }
    }
}