using Helion4x.Core;
using Helion4x.Scripts;
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

    public class AstronomicalBody : MonoBehaviour, IAstronomicalBody, IFollowable
    {
        [SerializeField] private MassType massType;
        [SerializeField] [Range(0, 64)] private int orbitSegments;
        [SerializeField] private GameObject parent;
        private CircularOrbit _circularOrbit;
        private CircularOrbitDrawer _circularOrbitDrawer;
        private LineRenderer _lineRenderer;
        private Vector3 _nextPositon;
        private IAstronomicalBody _parent;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            Mass = AstronomicalMass.ForMassType(massType);
            TimeManager.MinutePassed += OnMinutePassed;
        }

        private void Start()
        {
            if (parent == null) return;
            _parent = parent.GetComponent<IAstronomicalBody>();
            var radius =
                AstronomicalLength.FromMegameters(Vector3.Distance(_parent.transform.position,
                    transform.localPosition));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, _parent.Mass, OrbitType.Circular);
            _circularOrbit = new CircularOrbit(_parent, orbitalPeriod, radius);
            _circularOrbitDrawer = new CircularOrbitDrawer(_lineRenderer, _circularOrbit, orbitSegments);
            if (_lineRenderer == null) return;
            _circularOrbitDrawer.RenderOrbit();
        }

        public float Mass { get; private set; }

        public Transform FollowTransform => transform;

        private void OnMinutePassed()
        {
            if (_parent == null) return;
            _nextPositon = _circularOrbit.CalculateNextPosition(1);
            transform.localPosition = Vector3.Lerp(transform.position, _nextPositon, Time.deltaTime);
        }
    }
}