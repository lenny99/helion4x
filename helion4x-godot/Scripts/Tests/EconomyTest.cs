using System.Collections.Generic;
using Helion4x.Core.Settlement;
using Helion4x.Core.Settlement.Installation;
using Helion4x.Core.Settlement.Projects;
using WAT;

namespace Helion4x.Tests
{
    public class EconomyTest : Test
    {
        [Test]
        public void ProgressProjects()
        {
            var economy = new EconomyComponent(0.1f);
            var installationBonuses = new InstallationBonuses(new Dictionary<InstallationBonus, float>
            {
                [InstallationBonus.PrimarySectorJobs] = 2500
            });
            economy.CalculateGdp(1000, installationBonuses);
            Assert.IsTrue(economy.Gdp > 0);
            economy.BuildProject(new Project("Factory", 1000));
            for (var i = 1; i < 10; i++)
            {
                var finishedProjects = economy.ProgressProjects();
                if (finishedProjects.Count > 0)
                {
                    Assert.IsEqual(finishedProjects.Count, 1);
                    Assert.IsEqual(i, 1);
                    return;
                }
            }

            Assert.Fail("Project was not finished");
        }
    }
}