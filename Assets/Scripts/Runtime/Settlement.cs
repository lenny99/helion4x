using Helion4x.Configs;
using Helion4x.Core;
using Helion4x.Core.Settlement;
using Helion4x.Core.Settlement.Installation;
using UnityEngine;

namespace Helion4x.Runtime
{
    public class Settlement : MonoBehaviour, ISelectable
    {
        [SerializeField] private GameObject selectedMarker;
        [SerializeField] private SettlementObject start;

        private Economy _economy;
        private InstallationHolder _installationHolder;
        private PopulationSystem _populationSystem;
        public float Population => _populationSystem.Population;

        public void Start()
        {
            if (selectedMarker == null) Debug.LogWarning("Settlement Marker not assigned");
            _populationSystem = start.population.MakePopulation();
            _economy = start.economy.MakeEconomy();
            _installationHolder = start.MakeInstallationHolder();
            TimeManager.DayPassed += OnDayPassed;
            TimeManager.MonthPassed += OnMonthPassed;
        }

        public void Select()
        {
            selectedMarker.SetActive(true);
        }

        public void Unselect()
        {
            selectedMarker.SetActive(false);
        }

        private void OnDayPassed()
        {
            _economy.CalculateGdp(_populationSystem.Population, _installationHolder.GetBonuses());
            var finishedProjects = _economy.ProgressProjects();
            _installationHolder.AddProjects(finishedProjects);
        }

        private void OnMonthPassed()
        {
            _populationSystem.GrowPopulation(_installationHolder.GetBonuses());
        }
    }
}