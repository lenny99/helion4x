using System.Collections.Generic;
using System.Linq;
using Helion4x.Scripts.Settlement;
using Helion4x.Scripts.Settlement.Installation;
using Helion4x.Scripts.Settlement.Projects;
using NUnit.Framework;

namespace Helion4x_Tests
{
    public class EconomyTest
    {
        [Test]
        public void ProgressProjects()
        {
            var economy = new Economy(0.1f);
            var installationBonuses = new InstallationBonuses(new Dictionary<InstallationBonusType,float>()
            {
                [InstallationBonusType.PrimarySectorJobs] = 2500,
            });
            economy.CalculateGdp(1000, installationBonuses);
            Assert.True(economy.Gdp > 0);
            economy.BuildProject(new InfrastructureProject());
            for (int i = 1; i < 10; i++)
            {
                var finishedProjects = economy.ProgressProjects();
                if (finishedProjects.Count > 0)
                {
                    Assert.AreEqual(finishedProjects.Count, 1);
                    Assert.AreEqual(i, 1);
                    return;
                }
            }
            Assert.Fail("Project was not finished");
        }
    }
}