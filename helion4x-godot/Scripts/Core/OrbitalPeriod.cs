using System;

namespace Helion4x.Core
{
    public class OrbitalPeriod
    {
        private readonly OrbitType _orbitType;
        private readonly float _seconds;

        public OrbitalPeriod(double semiMajorAxis, float parentMass, OrbitType orbitType)
        {
            _orbitType = orbitType;
            _seconds = CalculatePeriodForOrbitType(semiMajorAxis, parentMass);
        }

        private float CalculatePeriodForOrbitType(double semiMajorAxis, float parentMass)
        {
            switch (_orbitType)
            {
                case OrbitType.Circular:
                    var dividend = 4 * Math.Pow(Math.PI, 2) * Math.Pow(semiMajorAxis, 3);
                    var divisor = AstronomicalConstants.G * parentMass;
                    return (float) Math.Sqrt(dividend / divisor);
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

        public float InMinutes()
        {
            return InSeconds() / 60;
        }

        public float InHours()
        {
            return InMinutes() / 60;
        }

        public float InDays()
        {
            return InHours() / 24;
        }
    }
}