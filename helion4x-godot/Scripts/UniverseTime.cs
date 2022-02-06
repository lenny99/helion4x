using System;
using Godot;
using Helion4x.Core.Time;

namespace Helion4x.Scripts
{
    public class UniverseTime : Node, ITimewarpTimeout
    {
        private DateTime _time;
        private NextUpdate _nextUpdate;

        private void SetTime(DateTime time)
        {
            _time = time;
        }

        public override void _Ready()
        {
            SetTime(DateTime.Parse("01/01/2030 00:00:00"));
            _nextUpdate = new NextUpdate(this._time);
        }
        
        public void OnTimerTimeout()
        {
            ProgressTime();
        }

        private void ProgressTime()
        {
            if (_nextUpdate.Hour <= _time)
            {
                _nextUpdate.Hour = _time.AddHours(1);
                ProgressTimeBy(Interval.Hour);
            }

            if (_nextUpdate.Day <= _time)
            {
                _nextUpdate.Day = _time.AddDays(1);
                ProgressTimeBy(Interval.Day);
            }

            if (_nextUpdate.Month <= _time)
            {
                _nextUpdate.Month = _time.AddMonths(1);
                ProgressTimeBy(Interval.Month);
            }

            if (_nextUpdate.Year <= _time)
            {
                _nextUpdate.Year = _time.AddYears(1);
                ProgressTimeBy(Interval.Year);
            }
            SetTime(_time.AddHours(1));
        }

        private void ProgressTimeBy(Interval interval)
        {
            GetTree().CallGroup("Timeables", "TimeProcess", interval);
        }
    }

    internal struct NextUpdate
    {
        public DateTime Hour { get; set; }
        public DateTime Day { get; set; }
        public DateTime Month { get; set; }
        public DateTime Year { get; set; }

        public NextUpdate(DateTime date)
        {
            Hour = date;
            Day = date;
            Month = date;
            Year = date;
        }
    }
}