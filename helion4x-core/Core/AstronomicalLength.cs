using UnitsNet;

namespace Helion4x.Core
{
    public class AstronomicalLength
    {
        public double Meters => _length.Meters;
        public double Kilometers => _length.Kilometers;
        public double Megameters => _length.Kilometers / 1000;

        private Length _length;

        private AstronomicalLength(Length length)
        {
            _length = length;
        }

        public static AstronomicalLength FromMeters(double meters)
        {
            return new AstronomicalLength(Length.FromMeters(meters));
        }
        
        public static AstronomicalLength OfKilometers(double kilometers)
        {
            return new AstronomicalLength(Length.FromKilometers(kilometers));
        }

        public static AstronomicalLength OfMegameters(double megameters)
        {
            return OfKilometers(megameters * 1000);
        }
    }
}