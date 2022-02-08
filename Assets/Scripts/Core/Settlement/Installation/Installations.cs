using System.Collections.Generic;
using Helion4x.Core.Settlement.Projects;

namespace Helion4x.Core.Settlement.Installation
{
    public class Installations
    {
        private List<Installation> _installations;

        public Installations()
        {
            _installations = new List<Installation>();
        }
        
        public InstallationBonuses GetBonuses()
        {
            var bonuses = new InstallationBonuses();
            foreach (var infrastructure in _installations)
            {
                bonuses.AddBonuses(infrastructure.GetBonuses());
            }
            return bonuses;
        }

        public void AddInstallations(List<Project> projects)
        {
            projects.ForEach(project => _installations.Add(project.Finish()));
        }
    }
}