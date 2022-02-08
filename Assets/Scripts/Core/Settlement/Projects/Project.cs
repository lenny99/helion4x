namespace Helion4x.Core.Settlement.Projects
{
    public abstract class Project
    {
        public string Name => _name;
        public bool IsFinished => _progress >= _cost;
        public bool IsNotFinished => !IsFinished;
        
        private string _name;
        private float _cost;
        private float _progress;

        public Project(string name, float cost)
        {
            _name = name;
            _cost = cost;
        }

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

        abstract public Installation.Installation Finish();
    }
}