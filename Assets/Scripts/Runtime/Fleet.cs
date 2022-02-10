using System.Collections.Generic;
using Helion4x.Core;
using UnitsNet;
using UnitsNet.Units;
using UnityEngine;

namespace Helion4x.Runtime
{
    public class Fleet : MonoBehaviour, IFollowable
    {
        [SerializeField] private GameObject parent;
        [SerializeField] private float movementTime;
        [SerializeField] [Range(16, 128)] private int orbitSegments;

        private CircularOrbit _circularOrbit;

        private LineRenderer _lineRenderer;
        private MovementType _movement;

        private Vector3 _nextPosition;
        private IAstronomicalBody _parent;
        private List<Acceleration> _ships;
        private Speed _speed;
        private Voyage _voyage;

        private Acceleration Acceleration => _ships?.Min(AccelerationUnit.MeterPerSecondSquared) != null
            ? _ships.Min(AccelerationUnit.MeterPerSecondSquared)
            : Acceleration.Zero;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void Start()
        {
            TimeManager.MinutePassed += OnMinutePassed;
            _ships = new List<Acceleration>(new[] {Acceleration.FromKilometersPerSecondSquared(1)});
            _parent = parent.GetComponent<IAstronomicalBody>();
            _movement = MovementType.Orbit;
            if (_parent == null) return;
            var radius =
                AstronomicalLength.FromMegameters(Vector3.Distance(_parent.transform.position, transform.position));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, _parent.Mass, OrbitType.Circular);
            _circularOrbit = new CircularOrbit(_parent, orbitalPeriod, radius);
            var circularObitDrawer = new CircularOrbitDrawer(_lineRenderer, _circularOrbit, orbitSegments);
            if (_lineRenderer == null) return;
            circularObitDrawer.RenderOrbit();
        }

        public Transform FollowTransform => transform;

        private void OnMinutePassed()
        {
            var newSpeed = _speed.KilometersPerSecond +
                           Acceleration.KilometersPerSecondSquared;
            _speed = Speed.FromKilometersPerMinutes(newSpeed);
            if (_movement == MovementType.Direct) transform.position = _voyage.NextPosition(1, _speed);
            if (_movement == MovementType.Orbit) transform.position = _circularOrbit.CalculateNextPosition(1);
        }
    }

    internal enum MovementType
    {
        Direct,
        Orbit
    }
}