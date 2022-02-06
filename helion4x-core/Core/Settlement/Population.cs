using System;
using Helion4x.Core.Settlement.Installation;

namespace Helion4x.Core.Settlement
{
    public class Population
    {
        private float _population;
        private float _carryCapacity;
        private float _growthRate = 1.02f;

        public void GrowPopulation(InstallationBonuses installationBonuses)
        {
            var carryCapacity = installationBonuses.Get(InstallationBonusType.CarryCapacity);
            _carryCapacity = carryCapacity;
            CalculatePopulationIncrease();
        }

        private void CalculatePopulationIncrease()
        {
            var K = _carryCapacity;
            var A = K - _population / _population;
            _population = (float) (K / 1 + A * Math.Pow(Math.E, _growthRate * 1));
        }

        public float GetPopulation()
        {
            return _population;
        }
    }
}
