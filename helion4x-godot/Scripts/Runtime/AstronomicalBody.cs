using Godot;
using Helion4x.Core;
using Helion4x.Scripts;

namespace Helion4x.Runtime
{
    public class AstronomicalBody : Spatial, IAstronomicalBody, IFollowable
    {
        //
        private CircularOrbit _circularOrbit;
        private IAstronomicalBody _parent;
        [Export] private MassType massType;
        [Export] private Node parent;

        public float Mass { get; private set; }

        public Vector3 FollowPosition => Translation;


        public override void _Ready()
        {
            Mass = AstronomicalMass.ForMassType(massType);
            TimeManager.MinutePassed += OnMinutePassed;
            if (parent == null) return;
            _parent = (IAstronomicalBody) parent;
            var radius =
                AstronomicalLength.FromMegameters(_parent.Translation.DistanceTo(Translation));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, _parent.Mass, OrbitType.Circular);
            _circularOrbit = new CircularOrbit(_parent, orbitalPeriod, radius);
        }

        private void OnMinutePassed()
        {
            if (_parent == null) return;
            Translation = _circularOrbit.CalculateNextPosition(1);
        }
    }
}