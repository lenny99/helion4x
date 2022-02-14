using System.Globalization;

namespace Helion4x.Core.Settlement
{
    public class Population
    {
        public float Count { get; }

        public Population(float count)
        {
            Count = count;
        }

        public static Population operator +(Population a, Population b) =>
            new Population(a.Count + b.Count);

        public override string ToString()
        {
            const double million = 1_000_000;
            if (Count > million)
            {
                return (Count / million).ToString(CultureInfo.InvariantCulture) + " mio";
            }

            const double thousand = 1_000;
            if (Count > thousand)
            {
                return (Count / thousand).ToString(CultureInfo.InvariantCulture) + " k";
            }
            return Count.ToString(CultureInfo.InvariantCulture);
        }
    }
}