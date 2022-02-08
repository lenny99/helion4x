using Helion4x.Core.Settlement;
using Helion4x.Core.Settlement.Installation;
using Helion4x.Core.Time;
using UnityEngine;

namespace Helion4x.Runtime
{
    public class Settlement : MonoBehaviour
    {
        private Economy _economy;
        private Installations _installations;
        private Population _population;

        public void Start()
        {
            _population = new Population();
            _economy = new Economy(0.2f);
            _installations = new Installations();
        }

        public void TimeProcess(Interval interval)
        {
            var bonuses = _installations.GetBonuses();
            var population = _population.GetPopulation();
            switch (interval)
            {
                case Interval.Day:
                    _economy.CalculateGdp(population, bonuses);
                    var finishedProjects = _economy.ProgressProjects();
                    _installations.AddInstallations(finishedProjects);
                    break;
                case Interval.Month:
                    _population.GrowPopulation(bonuses);
                    break;
            }
        }
    }
}