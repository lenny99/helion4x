using System.Collections.Generic;
using Godot;
using Helion4x.Core;
using Helion4x.Core.Time;
using UnitsNet;
using UnitsNet.Units;

namespace Helion4x.Scripts
{
    public class Fleet : Spatial, ITimeable
    {
        private CircularOrbiter _circularOrbiter;
        private MovementType _movement;
        [Export] private NodePath _parentNodePath;

        private List<Acceleration> _ships;
        private Voyage _voyage;
        private Translation target;

        public Acceleration Acceleration => _ships.Min(AccelerationUnit.MeterPerSecondSquared);

        public void TimeProcess(Interval interval)
        {
            if (interval != Interval.Minute) return;
            speed.KilometersPerMinutes += acceleration.KilometersPerSecondSquared / minutes;
            if (_movement == MovementType.Direct) {  }
        }

        public override void _Ready()
        {
            var parent = GetNode<IAstronomicalObject>(_parentNodePath);
            var radius = AstronomicalLength.FromMegameters(parent.Translation.DistanceTo(Translation));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, parent.Mass, OrbitType.Circular);
            _circularOrbiter = new CircularOrbiter(parent, orbitalPeriod, radius);
        }
    }

    public class Voyage
    {
        private readonly Vector3 _destination;
        private Vector3 _origin;
        private Vector3 _position;

        public Voyage(Vector3 origin, Vector3 destination)
        {
            _origin = origin;
            _destination = destination;
            _position = _origin;
        }

        public Vector3 NextPosition(int minutes, Speed speed)
        {
            var direction = _origin.DirectionTo(_destination);
            var movement = direction * (float) speed.KilometersPerMinutes * minutes;
            _position += movement;
            return _position;
        }
    }

    internal enum MovementType
    {
        Direct,
        Orbit
    }
}