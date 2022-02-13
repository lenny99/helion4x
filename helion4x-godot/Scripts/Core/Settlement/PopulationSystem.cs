using System;
using Helion4x.Core.Settlement.Installation;

namespace Helion4x.Core.Settlement
{
    public class PopulationSystem
    {
        private readonly float _growthRate = 1.02f;

        public PopulationSystem(float startPopulation, float growthRate)
        {
            Population = startPopulation;
            _growthRate = growthRate;
        }

        public float Population { get; private set; }

        public void GrowPopulation(InstallationBonuses installationBonuses)
        {
            var carryCapacity = installationBonuses.Get(InstallationBonusType.CarryCapacity);
            CalculatePopulationIncrease(carryCapacity);
        }

        private void CalculatePopulationIncrease(float carryCapacity)
        {
            var A = carryCapacity - Population / Population;
            Population = (float) (carryCapacity / 1 + A * Math.Pow(Math.E, _growthRate * 1));
        }
    }
}