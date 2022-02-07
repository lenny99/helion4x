using Godot;
using Helion4x.Core;
using Helion4x.Core.Time;
using UnitsNet;

namespace Helion4x.Scripts
{
    public class CircularOrbiter : Spatial, ITimeables
    {
        [Export] private readonly MassType _massType = MassType.Earth;
        [Export] private readonly NodePath _parentNodePath = new NodePath();
        private float _mass;
        private OrbitalPeriod _orbitalPeriod;
        private CircularOrbiter _parent;
        private AstronomicalLength _radius;
        private float _theta;

        public void TimeProcess(Interval interval)
        {
            if (interval == Interval.Minute && _parent != null) Translation = CalculateNextPosition(1);
        }

        public override void _Ready()
        {
            AddToGroup(nameof(ITimeables));
            _mass = Mass.ForMassType(_massType);
            if (_parentNodePath != null && _parentNodePath.IsEmpty()) return;

            _parent = GetNode<CircularOrbiter>(_parentNodePath);
            _radius = AstronomicalLength.OfMegameters(_parent.Translation.DistanceTo(Translation));
            _orbitalPeriod = new OrbitalPeriod(_radius.Meters, _parent._mass, OrbitType.Circular);
        }

        private Vector3 CalculateNextPosition(int minutes)
        {
            _theta -= 2 * Mathf.Pi / _orbitalPeriod.InMinutes() * minutes;
            if (Mathf.Abs(_theta) > 2 * Mathf.Pi) _theta += 2 * Mathf.Pi;

            return new Vector3(Mathf.Cos(_theta), 0, Mathf.Sin(_theta)) * (float) _radius.Megameters +
                   _parent.Translation;
        }
    }
}