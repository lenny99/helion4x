using Godot;
using Helion4x.Core.Time;

namespace Helion4x.Scripts
{
    public class TimewarpTimer : Timer
    {
        [Signal]
        public delegate void TimewarpTimeout();

        private ITimewarpState _prePauseTimewarp;

        private ITimewarpState _timewarp;

        public ITimewarpState Timewarp
        {
            get => _timewarp;
            set
            {
                _timewarp = value;
                WaitTime = value.GetWaitTime();
                Paused = value.IsPaused();
            }
        }

        public override void _Ready()
        {
            Timewarp = new Normal();
            _prePauseTimewarp = new Normal();
        }

        public void OnTimerTimeout()
        {
            EmitSignal(nameof(TimewarpTimeout));
        }

        public void SpeedUp()
        {
            Timewarp = Timewarp.SpeedUp();
        }

        public void SlowDown()
        {
            Timewarp = Timewarp.SlowDown();
        }

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed("pause")) Pause();
        }

        public void Pause()
        {
            if (Paused)
            {
                Timewarp = _prePauseTimewarp;
            }
            else
            {
                _prePauseTimewarp = _timewarp;
                Timewarp = new Paused();
            }
        }
    }
}