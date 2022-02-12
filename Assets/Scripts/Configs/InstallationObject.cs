using System;
using System.Collections.Generic;
using Helion4x.Core.Settlement.Installation;
using UnityEngine;

namespace Helion4x.Configs
{
    [Serializable]
    public struct InstallationBonusAndAmount
    {
        public InstallationBonusType bonusType;
        public int amount;
    }

    [CreateAssetMenu(fileName = "NewInstallation", menuName = "Settlement/Installation")]
    public class InstallationObject : ScriptableObject
    {
        public List<InstallationBonusAndAmount> bonuses;

        public InstallationBonuses ToInstallationBonus()
        {
            var value = new InstallationBonuses();
            foreach (var bonusObject in bonuses) value.AddBonus(bonusObject.bonusType, bonusObject.amount);
            return value;
        }
    }
}