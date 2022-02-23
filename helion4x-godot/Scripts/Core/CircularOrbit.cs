using System;
using System.Collections.Generic;
using Godot;

namespace Helion4x.Core
{
    public class CircularOrbit
    {
        private readonly IAstronomicalBody _parent;
        private readonly AstronomicalLength _radius;
        private float _theta;
        public TimeSpan OrbitalPeriod;

        public CircularOrbit(IAstronomicalBody parent, AstronomicalLength radius)
        {
            _parent = parent;
            _radius = radius;
            OrbitalPeriod = CalculateOrbitalPeriod();
        }

        public Vector3 CalculateNextPosition(int minutes)
        {
            _theta -= 2 * Mathf.Pi / OrbitalPeriod.Minutes * minutes;
            if (Mathf.Abs(_theta) > 2 * Mathf.Pi) _theta += 2 * Mathf.Pi;
            return PositionAtTheta(_theta);
        }

        private Vector3 PositionAtTheta(float theta)
        {
            return new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta)) * (float) _radius.Kilometers;
        }

        private TimeSpan CalculateOrbitalPeriod()
        {
            var dividend = 4 * Math.Pow(Math.PI, 2) * Math.Pow(_radius.Kilometers, 3);
            var divisor = AstronomicalConstants.G * _parent.Mass;
            var seconds = Math.Sqrt(dividend / divisor);
            return TimeSpan.FromSeconds(seconds);
        }

        public IEnumerable<Vector3> GetPositions(int divider)
        {
            var positions = new List<Vector3>();
            for (var i = 0; i < divider; i++)
            {
                var theta = 2 * Mathf.Pi / divider * i;
                positions.Add(PositionAtTheta(theta));
            }

            return positions;
        }
    }
}