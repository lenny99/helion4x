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
        public ITimewarpState SpeedUp()
        {
            return new Slowest();
        }

        public ITimewarpState SlowDown()
        {
            return new Paused();
        }

        public bool IsPaused()
        {
            return true;
        }

        public float GetWaitTime()
        {
            return 1;
        }

        public override string ToString()
        {
            return nameof(Paused);
        }
    }

    public class Slowest : ITimewarpState
    {
        public ITimewarpState SpeedUp()
        {
            return new Slow();
        }

        public ITimewarpState SlowDown()
        {
            return new Paused();
        }

        public bool IsPaused()
        {
            return false;
        }

        public float GetWaitTime()
        {
            return 2;
        }

        public override string ToString()
        {
            return nameof(Slowest);
        }
    }

    public class Slow : ITimewarpState
    {
        public ITimewarpState SpeedUp()
        {
            return new Normal();
        }

        public ITimewarpState SlowDown()
        {
            return new Slowest();
        }

        public bool IsPaused()
        {
            return false;
        }

        public float GetWaitTime()
        {
            return 1;
        }

        public override string ToString()
        {
            return nameof(Slow);
        }
    }

    public class Normal : ITimewarpState
    {
        public ITimewarpState SpeedUp()
        {
            return new Fast();
        }

        public ITimewarpState SlowDown()
        {
            return new Slow();
        }

        public bool IsPaused()
        {
            return false;
        }

        public float GetWaitTime()
        {
            return 0.01f;
        }

        public override string ToString()
        {
            return nameof(Normal);
        }
    }

    public class Fast : ITimewarpState
    {
        public ITimewarpState SpeedUp()
        {
            return new Fastest();
        }

        public ITimewarpState SlowDown()
        {
            return new Normal();
        }

        public bool IsPaused()
        {
            return false;
        }

        public float GetWaitTime()
        {
            return 0.01f;
        }

        public override string ToString()
        {
            return nameof(Fast);
        }
    }

    public class Fastest : ITimewarpState
    {
        public ITimewarpState SpeedUp()
        {
            return this;
        }

        public ITimewarpState SlowDown()
        {
            return new Fast();
        }

        public bool IsPaused()
        {
            return false;
        }

        public float GetWaitTime()
        {
            return 0.001f;
        }

        public override string ToString()
        {
            return nameof(Fastest);
        }
    }
}