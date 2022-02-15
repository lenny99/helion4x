using System.Globalization;
using Helion4x.Core.Settlement.Installation;

namespace Helion4x.Core.Settlement
{
    public class PopulationComponent
    {
        private readonly int _growthRate = 2;

        public PopulationComponent(float startPopulation, int growthRate)
        {
            Population = new Population(startPopulation);
            _growthRate = growthRate;
        }

        public Population Population { get; private set; }

        public void GrowPopulation(InstallationBonuses installationBonuses)
        {
            var carryCapacity = installationBonuses.Get(InstallationBonusType.CarryCapacity);
            CalculatePopulationIncrease(carryCapacity);
        }

        private void CalculatePopulationIncrease(float carryCapacity)
        {
            // var A = carryCapacity - Population.Amount / Population.Amount;
            // Population = (float) (carryCapacity / 1 + A * Math.Pow(Math.E, _growthRate * 1));
            var newPopulation = Population.GrowByPercent(2);
            Population = newPopulation.Amount > carryCapacity ? new Population(carryCapacity) : newPopulation;
        }
    }

    public class Population
    {
        public Population(float amount)
        {
            Amount = amount;
        }

        public float Amount { get; }

        public override string ToString()
        {
            const float million = 1_000_000f;
            if (Amount >= million)
                return Amount / million + " mio";

            const float thousand = 1_000f;
            if (Amount >= thousand)
                return Amount / thousand + " k";
            return Amount.ToString(CultureInfo.InvariantCulture);
        }

        public Population GrowByPercent(int percent)
        {
            return new Population(Amount * percent / 100);
        }
    }
}