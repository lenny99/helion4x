using System.Collections.Generic;

namespace Helion4x.Core.Settlement.Installation
{
    public class InstallationBonuses
    {
        private readonly Dictionary<InstallationBonus, float> _bonuses;

        public InstallationBonuses()
        {
            _bonuses = new Dictionary<InstallationBonus, float>();
        }

        public InstallationBonuses(Dictionary<InstallationBonus, float> bonuses)
        {
            _bonuses = bonuses;
        }

        public void AddBonuses(InstallationBonuses bonuses)
        {
            var dictionary = bonuses.GetBonuses();
            foreach (var key in dictionary.Keys) AddBonus(key, dictionary[key]);
        }

        private Dictionary<InstallationBonus, float> GetBonuses()
        {
            return _bonuses;
        }

        public void AddBonus(InstallationBonus type, float amount)
        {
            if (_bonuses.ContainsKey(type))
                _bonuses[type] += amount;
            else
                _bonuses[type] = amount;
        }

        public bool Contains(InstallationBonus type)
        {
            return _bonuses.ContainsKey(type);
        }

        public float Get(InstallationBonus type)
        {
            return _bonuses.ContainsKey(type) ? _bonuses[type] : 0;
        }
    }
}