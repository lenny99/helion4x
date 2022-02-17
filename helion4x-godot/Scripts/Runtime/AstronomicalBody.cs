using Godot;
using Helion4x.Core;
using Helion4x.Scripts;

namespace Helion4x.Runtime
{
    public class AstronomicalBody : Spatial, IAstronomicalBody, IFollowable
    {
        private CircularOrbit _circularOrbit;
        private Spatial _focus;
        private IAstronomicalBody _parent;

        public float Mass { get; private set; }

        public Spatial Followable => this;

        public override void _Ready()
        {
            Mass = AstronomicalMass.ForMassType(_massType);
            TimeManager.MinutePassed += OnMinutePassed;
            if (_parentPath == null) return;
            _parent = GetNode<IAstronomicalBody>(_parentPath);
            var radius =
                AstronomicalLength.FromMegameters(_parent.Translation.DistanceTo(Translation));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, _parent.Mass, OrbitType.Circular);
            _circularOrbit = new CircularOrbit(_parent, orbitalPeriod, radius);
        }

        private void OnMinutePassed()
        {
            Transform = Transform.Rotated(Vector3.Up, _rotation);
            if (_parent == null) return;
            Translation = _circularOrbit.CalculateNextPosition(1);
        }


        #region Exports

        [Export] private MassType _massType;
        [Export] private NodePath _parentPath;
        [Export] private float _rotation;

        #endregion
    }
}