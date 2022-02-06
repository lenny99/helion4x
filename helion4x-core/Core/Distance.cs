using System.Dynamic;

namespace Helion4x.Core
{
    public class Distance
    {
        public double Meters => _meters;
        public double Kilometers => _meters / 1000;
        public double Megameters => Kilometers / 1000;
        
        private double _meters;

        private Distance(double meters)
        {
            _meters = meters;
        }

        public static Distance OfMeters(double meters)
        {
            return new Distance(meters);
        }
        
        public static Distance OfKilometers(double kilometers)
        {
            return OfMeters(kilometers * 1000);
        }

        public static Distance OfMegameters(double megameters)
        {
            return OfKilometers(megameters * 1000);
        }
    }
}