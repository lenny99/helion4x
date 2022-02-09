using System;
using System.Collections.Generic;
using Helion4x.Core;
using Helion4x.Core.Time;
using UnitsNet;
using UnitsNet.Units;
using UnityEngine;

namespace Helion4x.Runtime
{
    public class Fleet : MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        private IAstronomicalObject _parent;
        
        private CircularOrbit _circularOrbit;
        private MovementType _movement;
        private List<Acceleration> _ships;
        private Speed _speed;
        private Voyage _voyage;

        private Acceleration Acceleration => _ships.Min(AccelerationUnit.MeterPerSecondSquared);

        public void Start()
        {
            TimeManager.MinutePassed += OnMinutePassed;
            _parent = parent.GetComponent<IAstronomicalObject>();
            if (_parent == null) return;
            var radius = AstronomicalLength.FromMegameters(Vector3.Distance(_parent.transform.position, transform.position));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, _parent.Mass, OrbitType.Circular);
            _circularOrbit = new CircularOrbit(_parent, orbitalPeriod, radius);
        }

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