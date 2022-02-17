using Helion4x.Core.Settlement.Installation;
using Helion4x.Core.Settlement.Sectors;

namespace Helion4x.Core.Settlement
{
    public class SecondarySector : Sector
    {
        public override float CalculateJobs(InstallationBonuses bonuses)
        {
            _jobs = bonuses.Get(InstallationBonus.SecondarySectorJobs);
            return Jobs;
        }
    }
}