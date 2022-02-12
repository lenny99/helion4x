namespace Helion4x.Core.Settlement.Projects
{
    public class Project
    {
        private readonly float _cost;

        private float _progress;

        public Project(string name, float cost)
        {
            Name = name;
            _cost = cost;
        }

        public string Name { get; }

        public bool IsFinished => _progress >= _cost;
        public bool IsNotFinished => !IsFinished;

        public float Progress(float budget)
        {
            var newProgress = _progress + budget;
            if (newProgress >= _cost)
            {
                _progress = _cost;
                return newProgress - _cost;
            }

            _progress = newProgress;
            return 0;
        }

        public Installation.Installation Finish()
        {
            return null;
        }
    }
}