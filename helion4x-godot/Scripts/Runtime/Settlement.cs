using System.Collections.Generic;
using Godot;
using Helion4x.Core.Settlement;
using Helion4x.Core.Settlement.Installation;

namespace Helion4x.Runtime
{
    public class Settlement : Node
    {
        private EconomyComponent _economyComponent;
        private InstallationComponent _installationComponent;
        private PopulationComponent _populationComponent;

        #region Exports

        [Export] private Resource start;

        #endregion

        public Population Population => _populationComponent.Population;

        public void Start()
        {
            _populationComponent = new PopulationComponent(1000000, 2);
            _economyComponent = new EconomyComponent(0.10f);
            _installationComponent = new InstallationComponent(new List<Installation>());
            TimeManager.DayPassed += OnDayPassed;
            TimeManager.MonthPassed += OnMonthPassed;
        }

        private void OnDayPassed()
        {
            _economyComponent.CalculateGdp(_populationComponent.Population.Amount, _installationComponent.GetBonuses());
            var finishedProjects = _economyComponent.ProgressProjects();
            _installationComponent.AddProjects(finishedProjects);
        }

        private void OnMonthPassed()
        {
            _populationComponent.GrowPopulation(_installationComponent.GetBonuses());
        }
    }
}