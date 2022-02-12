using UnityEngine;

namespace Helion4x.Runtime
{
    public class CircularOrbitDrawer
    {
        private readonly CircularOrbit _circularOrbit;
        private readonly LineRenderer _lineRenderer;
        private readonly int _segments;

        public CircularOrbitDrawer(LineRenderer lineRenderer, CircularOrbit circularOrbit, int segments)
        {
            _lineRenderer = lineRenderer;
            _circularOrbit = circularOrbit;
            _segments = segments;
        }

        public void RenderOrbit()
        {
            var points = new Vector3[_segments + 1];
            for (var i = 0; i < _segments; i++)
            {
                var angle = i / (float) _segments * 360 * Mathf.Deg2Rad;
                points[i] = _circularOrbit.GetPosition(angle);
            }

            points[_segments] = points[0];
            _lineRenderer.positionCount = _segments + 1;
            _lineRenderer.SetPositions(points);
        }
    }
}