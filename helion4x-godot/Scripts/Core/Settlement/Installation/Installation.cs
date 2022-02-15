namespace Helion4x.Core.Settlement.Installation
{
    public class Installation
    {
        private int _count;
        private string _name;

        public Installation(string name, int count, InstallationBonuses bonuses)
        {
            _name = name;
            _count = count;
            Bonuses = bonuses;
        }

        public InstallationBonuses Bonuses { get; }
    }
}