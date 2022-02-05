using System.Collections.Generic;
using Godot;
using Helion4x.Scripts.Settlement.Projects;

namespace Helion4x.Scripts.Settlement.Installation
{
    public class Installations : Node
    {
        private List<Installation> _installations;
        
        public override void _Ready()
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