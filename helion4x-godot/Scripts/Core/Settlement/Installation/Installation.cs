namespace Helion4x.Core.Settlement.Installation
{
    public class Installation
    {
        private int _count;

        public Installation(int count, InstallationBonuses bonuses)
        {
            _count = count;
            Bonuses = bonuses;
        }

        public InstallationBonuses Bonuses { get; }
    }
}