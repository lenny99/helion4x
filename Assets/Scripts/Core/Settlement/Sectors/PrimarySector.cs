using Helion4x.Core.Settlement.Installation;

namespace Helion4x.Core.Settlement.Sectors
{
    public class PrimarySector : Sector
    {
        public PrimarySector(float gdpPerCapita)
        {
            _gdpPerCapita = gdpPerCapita;
        }
        
        public override float CalculateJobs(InstallationBonuses bonuses)
        {
            _jobs = bonuses.Get(InstallationBonusType.PrimarySectorJobs);
            return Jobs;
        }
    }
}