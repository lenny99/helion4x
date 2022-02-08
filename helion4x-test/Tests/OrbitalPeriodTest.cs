using Helion4x.Core;
using Helion4x.Scripts;
using NUnit.Framework;

namespace Helion4x.Tests
{
    public class OrbitalPeriodTest
    {
        [Test]
        public void TestCircularOrbitalPeriod()
        {
            var orbitalPeriod = new OrbitalPeriod(AstronomicalConstants.Au, AstronomicalMass.ForMassType(MassType.Sun),
                OrbitType.Circular);
            Assert.AreEqual(365.202667f, orbitalPeriod.InDays());
        }
    }
}