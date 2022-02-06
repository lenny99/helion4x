using System.Collections.Generic;
using System.Linq;
using Helion4x.Core.Settlement.Installation;
using Helion4x.Core.Settlement.Projects;
using Helion4x.Core.Settlement.Sectors;

namespace Helion4x.Core.Settlement
{
    public class Economy
    {
        public float Gdp => _primarySector.GDP + _secondarySector.GDP + _tertiarySector.GDP;

        private readonly List<Project> _available_projects;
        private readonly Queue<Project> _projects_in_progress;
        private readonly Sector[] _sectors;
        private readonly Sector _primarySector;
        private readonly Sector _secondarySector;
        private readonly Sector _tertiarySector;

        private float _employed;
        private float _unemployed;
        private float _tax;

        public Economy(float tax)
        {
            _primarySector = new PrimarySector(2500);
            _secondarySector = new SecondarySector();
            _tertiarySector = new TertiarySector();
            _sectors = new[] {_primarySector, _secondarySector, _tertiarySector};
            _projects_in_progress = new Queue<Project>();
            _available_projects = new List<Project>(new[] {new InfrastructureProject()});
            _tax = tax;
        }

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
            return Foo(dailyBudget, finishedProjects);;
        }

        private List<Project> Foo(float dailyBudget, List<Project> finishedProjects)
        {
            var project = _projects_in_progress.Peek();
            dailyBudget = project.Progress(dailyBudget);
            if (project.IsFinished)
            {
                _projects_in_progress.Dequeue();
                finishedProjects.Add(project);
            }
            if (_projects_in_progress.Count == 0)
            {
                return finishedProjects;
            }
            if (dailyBudget > 0)
            {
                Foo(dailyBudget, finishedProjects);
            }
            return finishedProjects;
        }

        public void BuildProject(InfrastructureProject project)
        {
            _projects_in_progress.Enqueue(project);
        }
    }
}