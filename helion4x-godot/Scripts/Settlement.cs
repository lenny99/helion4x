using Godot;
using Helion4x.Core.Settlement;
using Helion4x.Core.Settlement.Installation;
using Helion4x.Core.Time;

namespace Helion4x.Scripts
{
    public class Settlement : Node, ITimeable, IEventInput
    {
        private Economy _economy;
        private Installations _installations;
        private Population _population;

        public void OnInputEvent(Node camera, InputEvent @event, Vector3 position, Vector3 normal, int shapeIndex)
        {
            var controller = new SettlementController();
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

        public override void _Ready()
        {
            AddToGroup(nameof(ITimeable));
            _population = new Population();
            _economy = new Economy(0.2f);
            _installations = new Installations();
        }
    }
}