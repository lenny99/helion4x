namespace Helion4x.Scripts.Settlement.Installation
{
    public abstract class Installation
    {
        private int _count;

        public abstract InstallationBonuses GetBonuses();
    }
}