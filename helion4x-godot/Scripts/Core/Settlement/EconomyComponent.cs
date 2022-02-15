using System.Collections.Generic;
using System.Linq;
using Helion4x.Core.Settlement.Installation;
using Helion4x.Core.Settlement.Projects;
using Helion4x.Core.Settlement.Sectors;

namespace Helion4x.Core.Settlement
{
    public class EconomyComponent
    {
        private readonly List<Project> _available_projects;
        private readonly Sector _primarySector;
        private readonly Queue<Project> _projects_in_progress;
        private readonly Sector _secondarySector;
        private readonly Sector[] _sectors;
        private readonly float _tax;
        private readonly Sector _tertiarySector;

        private float _employed;
        private float _unemployed;

        public EconomyComponent(float tax)
        {
            _primarySector = new PrimarySector(2500);
            _secondarySector = new SecondarySector();
            _tertiarySector = new TertiarySector();
            _sectors = new[] {_primarySector, _secondarySector, _tertiarySector};
            _projects_in_progress = new Queue<Project>();
            _tax = tax;
        }

        public float Gdp => _primarySector.GDP + _secondarySector.GDP + _tertiarySector.GDP;

        public void CalculateGdp(float population, InstallationBonuses bonuses)
        {
            var jobs = _sectors.Sum(sector => sector.CalculateJobs(bonuses));
            _unemployed = population - jobs;
            _employed = jobs;
        }

        public List<Project> ProgressProjects()
        {
            var finishedProjects = new List<Project>();
            var dailyBudget = Gdp * _tax / 365;
            while (_projects_in_progress.Any() && dailyBudget > 0)
            {
                var project = _projects_in_progress.Peek();
                dailyBudget = project.Progress(dailyBudget);
                if (!project.IsFinished) break;
                _projects_in_progress.Dequeue();
                finishedProjects.Add(project);
            }

            return finishedProjects;
        }

        public void BuildProject(Project project)
        {
            _projects_in_progress.Enqueue(project);
        }
    }
}