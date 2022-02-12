using System;
using System.Collections.Generic;
using System.Linq;
using Helion4x.Core.Settlement;
using Helion4x.Core.Settlement.Installation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Helion4x.Configs
{
    [Serializable]
    public struct InstallationAndAmount
    {
        public InstallationObject bonusObject;
        public int amount;

        public Installation ToInstallation()
        {
            return new Installation(amount, bonusObject.ToInstallationBonus());
        }
    }

    [Serializable]
    public struct EconomyConfig
    {
        public float tax;

        public Economy MakeEconomy()
        {
            return new Economy(tax);
        }
    }
    
    [Serializable]
    public struct PopulationConfig
    {
        public float population;
        public float growthRate;

        public PopulationSystem MakePopulation()
        {
            return new PopulationSystem(population, growthRate);
        }
    }

    [CreateAssetMenu(fileName = "NewSettlement", menuName = "Settlement/Settlement")]
    public class SettlementObject : ScriptableObject
    {
        public PopulationConfig population;
        public EconomyConfig economy;
        public List<InstallationAndAmount> installationsAndAmount;

        public InstallationHolder MakeInstallationHolder()
        {
            var installations = installationsAndAmount
                .Select(i => i.ToInstallation())
                .ToList();
            return new InstallationHolder(installations);
        }
    }
}