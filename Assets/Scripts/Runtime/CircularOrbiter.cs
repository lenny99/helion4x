using Helion4x.Core;
using UnityEngine;

namespace Helion4x.Runtime
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
            _theta -= 2 * Mathf.PI / _orbitalPeriod.InMinutes() * minutes;
            if (Mathf.Abs(_theta) > 2 * Mathf.PI) _theta += 2 * Mathf.PI;

            return new Vector3(
                Mathf.Cos(_theta),
                0,
                Mathf.Sin(_theta)
            ) * (float) _radius.Megameters + _parent.transform.position;
        }
    }
}