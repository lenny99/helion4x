using Godot;

namespace Helion4x.Util
{
    public static class SpatialExtensions
    {
        public static Vector3? GetPlanePosition(this Spatial node)
        {
            var plane = new Plane(Vector3.Up, 0);
            var mousePosition = node.GetViewport().GetMousePosition();
            var camera = node.GetViewport().GetCamera();
            var mouseWorldPosition = camera.ProjectPosition(mousePosition, 10000);
            return plane.IntersectRay(camera.Translation, mouseWorldPosition);
        }
    }
}