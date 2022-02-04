using System;
using Godot;

namespace Helion4x.Scripts
{
    public class CircularOrbiter : Spatial, ITimeable
    {
        [Export] private MassType _massType = MassType.Earth;
        [Export] private NodePath _parentNodePath;
        private CircularOrbiter _parent;
        private OrbitalPeriod _orbitalPeriod;
        private float _mass;
        private float _radius;
        private float _theta;

        public override void _Ready()
        {
            AddToGroup("Timeables");
            _mass = Mass.ForMassType(_massType);
            if (_parentNodePath.IsEmpty())
            {
                return;
            }
            _parent = GetNode<CircularOrbiter>(_parentNodePath);
            _radius = _parent.Translation.DistanceTo(Translation);
            _orbitalPeriod = new OrbitalPeriod(_radius, _parent._mass, OrbitType.Circular);
        }

        public void TimeProcess(Interval interval)
        {
            if (interval == Interval.Hour && _parent != null)
            {
                Translation = CalculateNextPosition(1);
            }
        }

        private Vector3 CalculateNextPosition(int hours)
        {
            _theta -= 2 * Mathf.Pi / _orbitalPeriod.InHours() * hours;
            if (Mathf.Abs(_theta) > 2 * Mathf.Pi)
            {
                _theta += 2 * Mathf.Pi;
            }
            return new Vector3(Mathf.Cos(_theta), 0, Mathf.Sin(_theta)) * _radius + _parent.Translation;
        }
    }

    public enum MassType
    {
        Sun,
        Earth,
        Moon
    }

    public static class Mass
    {
        private const float Sun = 1.989e30f;
        private const float Earth = 5.972e24f;
        private const float Moon = 7.342e22f;

        public static float ForMassType(MassType massType)
        {
            switch (massType)
            {
                case MassType.Sun:
                    return Mass.Sun;
                case MassType.Earth:
                    return Mass.Earth;
                case MassType.Moon:
                    return Mass.Moon;
                default:
                    return 0f;
            }
        }
    }
}