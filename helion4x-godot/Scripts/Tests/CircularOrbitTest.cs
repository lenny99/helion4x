using Helion4x.Core;
using Helion4x.Runtime;
using Helion4x.Scripts;
using UnitsNet;
using WAT;

namespace Helion4x.Tests
{
    public class CircularOrbitTest : Test
    {
        [Test]
        public void TestCircularOrbitalPeriod()
        {
            var parent = new AstronomicalBody();
            parent.MassType = MassType.Sun;
            var orbit = new CircularOrbit(parent, AstronomicalLength.FromLength(Length.FromAstronomicalUnits(1)));
            Assert.IsEqual(365.202667f, orbit.OrbitalPeriod.Days);
        }
    }
}