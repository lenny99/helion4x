using Godot;
using Helion4x.Core.Time;

namespace Helion4x.Scripts
{
    public class TimewarpTimer : Timer
    {
        [Signal]
        public delegate void TimewarpTimeout();
        
        private ITimewarpState _timewarp;
        private ITimewarpState _prePauseTimewarp;

        public void OnTimerTimeout()
        {
            EmitSignal(nameof(TimewarpTimeout));
        }

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed("pause"))
            {
                Pause();
            }
        }

        private void Pause()
        {
            if (Paused)
            {
                _timewarp = _prePauseTimewarp;
                SetWaitTimeForTimewarp();
            }
            else
            {
                _prePauseTimewarp = _timewarp;
                _timewarp = new Paused();
                SetWaitTimeForTimewarp();
            }
        }

        private void SetWaitTimeForTimewarp()
        {
            WaitTime = _timewarp.GetWaitTime();
            Paused = _timewarp.IsPaused();
        }
    }
}