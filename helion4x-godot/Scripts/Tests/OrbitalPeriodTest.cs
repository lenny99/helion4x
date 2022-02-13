using Helion4x.Core;
using Helion4x.Scripts;
using WAT;

namespace Helion4x.Tests
{
    public class OrbitalPeriodTest : WAT.Test
    {
        [Test]
        public void TestCircularOrbitalPeriod()
        {
            var orbitalPeriod = new OrbitalPeriod(AstronomicalConstants.Au, AstronomicalMass.ForMassType(MassType.Sun),
                OrbitType.Circular);
            Assert.IsEqual(365.202667f, orbitalPeriod.InDays());
        }
    }
}