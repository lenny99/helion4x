namespace Helion4x.Core.Time
{
    public interface ITimewarpState
    {
        bool Paused { get; }
        float Duration { get; }
        ITimewarpState SpeedUp();
        ITimewarpState SlowDown();
    }

    public abstract class BaseTimewarpState
    {
        private protected float DurationPerTick(float timesPerTick)
        {
            return 1 / timesPerTick;
        }
    }

    public class PausedState : BaseTimewarpState, ITimewarpState
    {
        public PausedState()
        {
            Paused = true;
            Duration = DurationPerTick(1);
        }

        public ITimewarpState SpeedUp()
        {
            return new OneSpeed();
        }

        public ITimewarpState SlowDown()
        {
            return new PausedState();
        }

        public bool Paused { get; }
        public float Duration { get; }

        public override string ToString()
        {
            return "(x0)";
        }
    }

    public class OneSpeed : BaseTimewarpState, ITimewarpState
    {
        private const float SixtyTimesPerSecond = 1;

        public OneSpeed()
        {
            Paused = false;
            Duration = DurationPerTick(60);
        }

        public ITimewarpState SpeedUp()
        {
            return new TwoSpeed();
        }

        public ITimewarpState SlowDown()
        {
            return new PausedState();
        }

        public bool Paused { get; }
        public float Duration { get; }

        public override string ToString()
        {
            return "(1x)";
        }
    }

    public class TwoSpeed : BaseTimewarpState, ITimewarpState
    {
        public TwoSpeed()
        {
            Paused = false;
            Duration = DurationPerTick(120);
        }

        public ITimewarpState SpeedUp()
        {
            return new ThreeSpeed();
        }

        public ITimewarpState SlowDown()
        {
            return new OneSpeed();
        }

        public bool Paused { get; }
        public float Duration { get; }

        public override string ToString()
        {
            return "(2x)";
        }
    }

    public class ThreeSpeed : BaseTimewarpState, ITimewarpState
    {
        public ThreeSpeed()
        {
            Paused = false;
            Duration = DurationPerTick(240);
        }

        public bool Paused { get; }
        public float Duration { get; }

        public ITimewarpState SpeedUp()
        {
            return this;
        }

        public ITimewarpState SlowDown()
        {
            return new TwoSpeed();
        }

        public override string ToString()
        {
            return "(3x)";
        }
    }
}