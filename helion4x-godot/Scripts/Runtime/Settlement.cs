using Godot;
using Helion4x.Core.Settlement;
using Helion4x.Core.Settlement.Installation;
using Helion4x.Editor;

namespace Helion4x.Runtime
{
    public class Settlement : Node
    {
        private EconomyComponent _economyComponent;
        private InstallationComponent _installationComponent;
        private PopulationComponent _populationComponent;

        #region Exports

        [Export] private SettlementResource _settlementResource;

        #endregion

        public Population Population => _populationComponent.Population;
        public float Gdp => _economyComponent.Gdp;
        public float Tax => _economyComponent.Tax;

        public override void _Ready()
        {
            if (_settlementResource == null)
            {
                GD.PushWarning($"SettlementResource not assigned to ${Name}. Disabling node...");
                PauseMode = PauseModeEnum.Stop;
            }

            _populationComponent = _settlementResource.MakePopulation();
            _economyComponent = _settlementResource.MakeEconomy();
            _installationComponent = _settlementResource.MakeInstallations();
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