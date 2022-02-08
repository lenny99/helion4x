using System;
using Godot;
using Helion4x.Core.Time;

namespace Helion4x.Scripts
{
    public class UniverseTime : Node, ITimewarpTimeout
    {
        private NextUpdate _nextUpdate;

        private DateTime _time;
        public DateTime Time => _time;

        public void OnTimerTimeout()
        {
            ProgressTime();
        }

        private void SetTime(DateTime time)
        {
            _time = time;
        }

        public override void _Ready()
        {
            SetTime(DateTime.Parse("01/01/2030 00:00:00"));
            _nextUpdate = new NextUpdate(_time);
        }

        private void ProgressTime()
        {
            if (_nextUpdate.Minute <= _time)
            {
                _nextUpdate.Minute = _time.AddMinutes(1);
                ProgressTimeBy(Interval.Minute);
            }

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

            SetTime(_time.AddMinutes(1));
        }

        private void ProgressTimeBy(Interval interval)
        {
            GetTree().CallGroup(nameof(ITimeable), nameof(ITimeable.TimeProcess), interval);
        }
    }

    internal struct NextUpdate
    {
        public DateTime Minute;
        public DateTime Hour;
        public DateTime Day;
        public DateTime Month;
        public DateTime Year;

        public NextUpdate(DateTime date)
        {
            Minute = date;
            Hour = date;
            Day = date;
            Month = date;
            Year = date;
        }
    }
}