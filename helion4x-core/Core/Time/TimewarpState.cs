namespace Helion4x.Core.Time
{
    public interface ITimewarpState
    {
        ITimewarpState SpeedUp();
        ITimewarpState SlowDown();
        bool IsPaused();
        float GetWaitTime();
    }

    public class Paused : ITimewarpState
    {
        public ITimewarpState SpeedUp() => new Slowest();
        public ITimewarpState SlowDown() => new Paused();
        public bool IsPaused() => true;
        public float GetWaitTime() => 1;
    }

    public class Slowest : ITimewarpState
    {
        public ITimewarpState SpeedUp() => new Slow();
        public ITimewarpState SlowDown() => new Paused();
        public bool IsPaused() => false;
        public float GetWaitTime() => 4;
    }

    public class Slow : ITimewarpState
    {
        public ITimewarpState SpeedUp() => new Normal();
        public ITimewarpState SlowDown() => new Slowest();
        public bool IsPaused() => false;
        public float GetWaitTime() => 2;
    }

    public class Normal : ITimewarpState
    {
        public ITimewarpState SpeedUp() => new Fast();
        public ITimewarpState SlowDown() => new Slow();
        public bool IsPaused() => false;
        public float GetWaitTime() => 1;
    }

    public class Fast : ITimewarpState
    {
        public ITimewarpState SpeedUp() => new Fastest();
        public ITimewarpState SlowDown() => new Normal();
        public bool IsPaused() => false;
        public float GetWaitTime() => 0.5f;
    }

    public class Fastest : ITimewarpState
    {
        public ITimewarpState SpeedUp() => this;
        public ITimewarpState SlowDown() => new Fast();
        public bool IsPaused() => false;
        public float GetWaitTime() => 0.1f;
    }
}