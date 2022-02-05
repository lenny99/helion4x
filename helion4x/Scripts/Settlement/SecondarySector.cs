using Helion4x.Scripts.Settlement.Installation;

namespace Helion4x.Scripts.Settlement
{
    public class SecondarySector : Sector
    {
        public override float CalculateJobs(InstallationBonuses bonuses)
        {
            _jobs = bonuses.Get(InstallationBonusType.SecondarySectorJobs);
            return Jobs;
        }
    }
}