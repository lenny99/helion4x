using System;
using Godot;
using Helion4x.Scripts.Settlement.Installation;

namespace Helion4x.Scripts.Settlement
{
    public class Population
    {
        private float _population = 0;
        private float _carryCapacity = 0;
        private float _growthRate = 1.02f;

        public void GrowPopulation(InstallationBonuses installationBonuses)
        {
            var carryCapacity = installationBonuses.Get(InstallationBonusType.CarryCapacity);
            if (carryCapacity != null)
            {
                _carryCapacity = carryCapacity;
            }
            CalculatePopulationIncrease();
        }

        private void CalculatePopulationIncrease()
        {
            var K = _carryCapacity;
            var A = K - _population / _population;
            _population = K / 1 + A * Mathf.Pow(Mathf.E, _growthRate * 1);
        }

        public float GetPopulation()
        {
            return _population;
        }
    }
}
