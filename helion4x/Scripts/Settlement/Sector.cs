using Helion4x.Scripts.Settlement.Installation;

namespace Helion4x.Scripts.Settlement
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