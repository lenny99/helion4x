using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Helion4x.Scripts.Settlement.Installation
{
    public class InstallationBonuses
    {
        private readonly Dictionary<InstallationBonusType, float> _bonuses;

        public InstallationBonuses()
        {
            _bonuses = new Dictionary<InstallationBonusType, float>();
        }

        public InstallationBonuses(Dictionary<InstallationBonusType, float> bonuses)
        {
            _bonuses = bonuses;
        }

        public void AddBonuses(InstallationBonuses bonuses)
        {
            var dictionary = bonuses.GetBonuses();
            foreach (var key in dictionary.Keys)
            {
                AddBonus(key, dictionary[key]);
            }
        }

        private Dictionary<InstallationBonusType, float> GetBonuses()
        {
            return _bonuses;
        }

        private void AddBonus(InstallationBonusType type, float amount)
        {
            if (_bonuses.ContainsKey(type))
            {
                _bonuses[type] += amount;
            }
            else
            {
                _bonuses[type] = amount;
            }
        }

        public bool Contains(InstallationBonusType type)
        {
            return _bonuses.ContainsKey(type);
        }

        public float Get(InstallationBonusType type)
         {
            return _bonuses.ContainsKey(type) ? _bonuses[type] : 0;
        }
    }
}