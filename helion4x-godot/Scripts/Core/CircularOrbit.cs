using Godot;

namespace Helion4x.Core
{
    public class CircularOrbit
    {
        private readonly OrbitalPeriod _orbitalPeriod;
        private readonly IAstronomicalBody _parent;
        private readonly AstronomicalLength _radius;
        private float _theta;

        public CircularOrbit(IAstronomicalBody parent, OrbitalPeriod orbitalPeriod, AstronomicalLength radius)
        {
            _parent = parent;
            _orbitalPeriod = orbitalPeriod;
            _radius = radius;
        }

        public Vector3 CalculateNextPosition(int minutes)
        {
            _theta -= 2 * Mathf.Pi / _orbitalPeriod.InMinutes() * minutes;
            if (Mathf.Abs(_theta) > 2 * Mathf.Pi) _theta += 2 * Mathf.Pi;

            return new Vector3(Mathf.Cos(_theta), 0, Mathf.Sin(_theta)) * (float) _radius.Megameters;
        }

        public Vector3 GetPosition(float angle)
        {
            return new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * (float) _radius.Megameters;
        }
    }
}