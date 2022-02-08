using System.Collections.Generic;

namespace Helion4x.Core.Settlement.Installation
{
    public class Infrastructure : Installation
    {
        public override InstallationBonuses GetBonuses()
        {
            return new InstallationBonuses(new Dictionary<InstallationBonusType, float>
            {
                [InstallationBonusType.CarryCapacity] = 1000
            });
        }
    }
}