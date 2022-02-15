using System.Collections.Generic;
using Godot;
using Helion4x.Core;
using UnitsNet;
using UnitsNet.Units;

namespace Helion4x.Runtime
{
    public class Fleet : Spatial, IFollowable
    {
        private CircularOrbit _circularOrbit;
        private MovementType _movement;
        private Vector3 _nextPosition;
        private IAstronomicalBody _parent;
        private List<Acceleration> _ships;
        private Speed _speed;
        private Voyage _voyage;

        private Acceleration Acceleration => _ships?.Min(AccelerationUnit.MeterPerSecondSquared) != null
            ? _ships.Min(AccelerationUnit.MeterPerSecondSquared)
            : Acceleration.Zero;

        public Vector3 FollowPosition => Translation;

        public override void _Ready()
        {
            TimeManager.MinutePassed += OnMinutePassed;
            _ships = new List<Acceleration>(new[] {Acceleration.FromKilometersPerSecondSquared(1)});
            _parent = GetNode<IAstronomicalBody>(_parentPath);
            _movement = MovementType.Orbit;
            if (_parent == null) return;
            var radius =
                AstronomicalLength.FromMegameters(_parent.Translation.DistanceTo(Translation));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, _parent.Mass, OrbitType.Circular);
            _circularOrbit = new CircularOrbit(_parent, orbitalPeriod, radius);
        }

        private void OnMinutePassed()
        {
            var newSpeed = _speed.KilometersPerSecond +
                           Acceleration.KilometersPerSecondSquared;
            _speed = Speed.FromKilometersPerMinutes(newSpeed);
            if (_movement == MovementType.Direct) Translation = _voyage.NextPosition(1, _speed);
            if (_movement == MovementType.Orbit) Translation = _circularOrbit.CalculateNextPosition(1);
        }

        #region Exports

        [Export] private NodePath _parentPath;
        [Export] private float _movementTime;
        [Export] private int _orbitSegments;

        #endregion
    }

    internal enum MovementType
    {
        Direct,
        Orbit
    }
}