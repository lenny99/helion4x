using Helion4x.Core.Settlement.Installation;

namespace Helion4x.Core.Settlement.Sectors
{
    public abstract class Sector
    {
        protected float _jobs;
        protected float _gdpPerCapita;
        public abstract float CalculateJobs(InstallationBonuses bonuses);

        public float Jobs => _jobs;

        public float GDP => _jobs * _gdpPerCapita;
    }
}