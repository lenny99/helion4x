namespace Helion4x.Core.Settlement.Installation
{
    public abstract class Installation
    {
        private int _count;

        public abstract InstallationBonuses GetBonuses();
    }
}