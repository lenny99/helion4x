using System.Collections.Generic;
using Godot;
using Helion4x.Core;
using Helion4x.Core.Settlement;
using Helion4x.Core.Settlement.Installation;

namespace Helion4x.Runtime
{
    public class Settlement : Node, ISelectable
    {
        [Export()] private Resource start;

        private EconomySystem _economySystem;
        private InstallationHolder _installationHolder;
        private PopulationSystem _populationSystem;
        public float Population => _populationSystem.Population;

        public void Start()
        {
            _populationSystem = new PopulationSystem(1000000, 0.02f);
            _economySystem = new EconomySystem(0.10f);
            _installationHolder = new InstallationHolder(new List<Installation>());
            TimeManager.DayPassed += OnDayPassed;
            TimeManager.MonthPassed += OnMonthPassed;
        }

        private void OnDayPassed()
        {
            _economySystem.CalculateGdp(_populationSystem.Population, _installationHolder.GetBonuses());
            var finishedProjects = _economySystem.ProgressProjects();
            _installationHolder.AddProjects(finishedProjects);
        }

        private void OnMonthPassed()
        {
            _populationSystem.GrowPopulation(_installationHolder.GetBonuses());
        }

        public void Select()
        {
            throw new System.NotImplementedException();
        }

        public void Unselect()
        {
            throw new System.NotImplementedException();
        }
    }
}