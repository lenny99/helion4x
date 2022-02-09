using System;
using System.Xml.Serialization;
using Helion4x.Core;
using Helion4x.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Helion4x.Runtime
{
    public class AstronomicalBody : MonoBehaviour, IAstronomicalBody, IFollowable
    {
        private LineRenderer _lineRenderer;

        [SerializeField] private MassType massType;
        [SerializeField, Range(8, 64)] private int orbitSegments;
        [SerializeField] private GameObject parent;
        private CircularOrbit _circularOrbit;
        private IAstronomicalBody _parent;

        public float Mass { get; private set; }

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            Mass = AstronomicalMass.ForMassType(massType);
            TimeManager.MinutePassed += OnMinutePassed;
            if (parent == null) return;
            _parent = parent.GetComponent<IAstronomicalBody>();
            var radius =
                AstronomicalLength.FromMegameters(Vector3.Distance(_parent.transform.position, transform.localPosition));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, _parent.Mass, OrbitType.Circular);
            _circularOrbit = new CircularOrbit(_parent, orbitalPeriod, radius);
        }

        private void OnMinutePassed()
        {
            if (_parent == null) return;
            transform.localPosition = _circularOrbit.CalculateNextPosition(1);
            if (_lineRenderer == null) return;
            RenderOrbit();
        }

        private void RenderOrbit()
        {
            var points = new Vector3[orbitSegments + 1];
            for (var i = 0; i < orbitSegments; i++)
            {
                var angle = i / (float) orbitSegments * 360 * Mathf.Deg2Rad;
                points[i] = _circularOrbit.GetPosition(angle);
            }

            points[orbitSegments] = points[0];
            _lineRenderer.positionCount = orbitSegments + 1;
            _lineRenderer.SetPositions(points);
        }

        public Transform Transform => transform;
    }
}