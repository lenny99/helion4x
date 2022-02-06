using System;

namespace Helion4x.Core
{
    public class OrbitalPeriod
    {
        private const float G = 6.67430e-11f;

        private readonly OrbitType _orbitType;
        private readonly float _seconds;

        public OrbitalPeriod(float semiMajorAxis, float parentMass, OrbitType orbitType)
        {
            _orbitType = orbitType;
            _seconds = CalculatePeriodForOrbitType(semiMajorAxis, parentMass);
        }

        private float CalculatePeriodForOrbitType(float semiMajorAxis, float parentMass)
        {
            switch (_orbitType)
            {
                case OrbitType.Circular:
                    return (float) Math.Sqrt(4 * Math.Pow(Math.PI, 2) * Math.Pow(semiMajorAxis, 3) / G * parentMass);
                case OrbitType.Elliptical:
                    return 0;
                default:
                    return 0;
            }
        }

        public float InSeconds()
        {
            return _seconds;
        }

        public float InHours()
        {
            return InSeconds() / 60 / 60;
        }

        public float InDays()
        {
            return InHours() / 24;
        }
    }
}