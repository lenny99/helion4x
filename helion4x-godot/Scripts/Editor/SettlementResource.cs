using System.Collections.Generic;
using Godot;
using Helion4x.Core.Settlement;
using Helion4x.Core.Settlement.Installation;
using MonoCustomResourceRegistry;

namespace Helion4x.Editor
{
    [RegisteredType(nameof(SettlementResource))]
    public class SettlementResource : Resource
    {
        [Export] private int[] _installationAmount = { };
        [Export] private InstallationResource[] _installations = { };
        [Export] public int GrowthRate;
        [Export] public float Population;
        [Export] public float Tax;

        public PopulationComponent MakePopulation()
        {
            return new PopulationComponent(Population, GrowthRate);
        }

        public EconomyComponent MakeEconomy()
        {
            return new EconomyComponent(Tax);
        }

        public InstallationComponent MakeInstallations()
        {
            var installations = new List<Installation>();
            for (var i = 0; i < _installations.Length; i++)
            {
                var installation = _installations[i];
                var count = _installationAmount[i];
                var bonuses = new InstallationBonuses();
                for (var j = 0; j < installation.Bonuses.Length; j++)
                {
                    var bonus = installation.Bonuses[j];
                    var amount = installation.Count[j];
                    bonuses.AddBonus(bonus, amount);
                }

                installations.Add(new Installation(null, count, bonuses));
            }

            return new InstallationComponent(installations);
        }
    }
}