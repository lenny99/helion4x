using Godot;
using Helion4x.Core;
using Helion4x.Core.Time;

namespace Helion4x.Scripts
{
    public class CircularOrbiter
    {
        private readonly OrbitalPeriod _orbitalPeriod;
        private readonly IAstronomicalObject _parent;
        private readonly AstronomicalLength _radius;
        private float _theta;

        public CircularOrbiter(IAstronomicalObject parent, OrbitalPeriod orbitalPeriod, AstronomicalLength radius)
        {
            _parent = parent;
            _orbitalPeriod = orbitalPeriod;
            _radius = radius;
        }

        public Vector3 CalculateNextPosition(int minutes)
        {
            _theta -= 2 * Mathf.Pi / _orbitalPeriod.InMinutes() * minutes;
            if (Mathf.Abs(_theta) > 2 * Mathf.Pi) _theta += 2 * Mathf.Pi;

            return new Vector3(Mathf.Cos(_theta), 0, Mathf.Sin(_theta)) *
                (float) _radius.Megameters + _parent.Translation;
        }
    }

    public interface IAstronomicalObject
    {
        float Mass { get; }
        Vector3 Translation { get; }
    }

    public class AstronomicalObject : Spatial, ITimeable, IAstronomicalObject
    {
        [Export] private readonly MassType _massType = MassType.Earth;
        [Export] private readonly NodePath _parentNodePath = new NodePath();
        private CircularOrbiter _circularOrbiter;
        private IAstronomicalObject _parent;

        public float Mass { get; private set; }

        public void TimeProcess(Interval interval)

        {
            if (interval == Interval.Minute && _parent != null) Translation = _circularOrbiter.CalculateNextPosition(1);
        }

        public override void _Ready()
        {
            AddToGroup(nameof(ITimeable));
            Mass = AstronomicalMass.ForMassType(_massType);
            if (_parentNodePath != null && _parentNodePath.IsEmpty()) return;

            _parent = GetNode<IAstronomicalObject>(_parentNodePath);
            var radius = AstronomicalLength.FromMegameters(_parent.Translation.DistanceTo(Translation));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, _parent.Mass, OrbitType.Circular);
            _circularOrbiter = new CircularOrbiter(_parent, orbitalPeriod, radius);
        }
    }
}