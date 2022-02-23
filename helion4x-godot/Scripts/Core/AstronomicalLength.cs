using UnitsNet;

namespace Helion4x.Core
{
    public class AstronomicalLength
    {
        private Length _length;

        private AstronomicalLength(Length length)
        {
            _length = length;
        }

        public double Meters => _length.Meters;
        public double Kilometers => _length.Kilometers;
        public double Megameters => _length.Kilometers / 1000;

        public static AstronomicalLength FromLength(Length length)
        {
            return new AstronomicalLength(length);
        }


        public static AstronomicalLength FromMeters(double meters)
        {
            return new AstronomicalLength(Length.FromMeters(meters));
        }

        public static AstronomicalLength FromKilometers(double kilometers)
        {
            return new AstronomicalLength(Length.FromKilometers(kilometers));
        }

        public static AstronomicalLength FromMegameters(double megameters)
        {
            return FromKilometers(megameters * 1000);
        }
    }
}