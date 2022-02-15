using System.Collections.Generic;
using Helion4x.Core.Settlement.Projects;

namespace Helion4x.Core.Settlement.Installation
{
    public class InstallationComponent
    {
        private readonly List<Installation> _installations;

        public InstallationComponent(List<Installation> installations)
        {
            _installations = installations;
        }

        public InstallationBonuses GetBonuses()
        {
            var bonuses = new InstallationBonuses();
            foreach (var infrastructure in _installations) bonuses.AddBonuses(infrastructure.Bonuses);
            return bonuses;
        }

        public void AddProjects(List<Project> projects)
        {
            projects.ForEach(project => _installations.Add(project.Finish()));
        }
    }
}