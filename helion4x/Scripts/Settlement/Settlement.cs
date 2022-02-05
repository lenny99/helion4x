using Godot;
using Helion4x.Scripts.Settlement.Installation;

namespace Helion4x.Scripts.Settlement
{
    public class Settlement : Node, ITimeable
    {
        private Population _population;
        private Economy _economy;
        private Installations _installations;

        public override void _Ready()
        {
            AddToGroup("Timeables");
        }

        public void TimeProcess(Interval interval)
        {
            var bonuses = _installations.GetBonuses();
            var population = _population.GetPopulation();
            switch (interval)
            {
                case Interval.Day:
                    _economy.CalculateGdp(population ,bonuses);
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