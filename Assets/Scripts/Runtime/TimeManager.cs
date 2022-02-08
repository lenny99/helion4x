using System;
using UnityEngine;
using UnityTimer;

namespace Helion4x.Runtime
{
    public class TimeManager : MonoBehaviour
    {
        public static event Action MinutePassed = delegate {  };
        public static event Action HourPassed = delegate {  };
        public static event Action DayPassed = delegate {  };
        public static event Action MonthPassed = delegate {  };
        public static event Action YearPassed = delegate {  };
        
        public DateTime Time => _time;
        private NextUpdate _nextUpdate;
        private DateTime _time;
        private Timer _timer;

        private void Start()
        {
            _time = DateTime.Parse("01/01/2030 00:00:00");
            _nextUpdate = new NextUpdate(_time);
            _timer  = Timer.Register(0.1f, ProgressTime, isLooped: true);
        }

        public void Pause()
        {
            if (_timer.isPaused) _timer.Resume();
            else  _timer.Pause();
        }

        private void ProgressTime()
        {
            if (_nextUpdate.minute <= _time)
            {
                _nextUpdate.minute = _time.AddMinutes(1);
                MinutePassed();
            }

            if (_nextUpdate.hour <= _time)
            {
                _nextUpdate.hour = _time.AddHours(1);
                HourPassed();
            }

            if (_nextUpdate.day <= _time)
            {
                _nextUpdate.day = _time.AddDays(1);
                DayPassed();
            }

            if (_nextUpdate.month <= _time)
            {
                _nextUpdate.month = _time.AddMonths(1);
                MonthPassed();
            }

            if (_nextUpdate.year <= _time)
            {
                _nextUpdate.year = _time.AddYears(1);
                YearPassed();
            }

            _time = _time.AddMinutes(1);
        }
    }

    internal struct NextUpdate
    {
        public DateTime minute;
        public DateTime hour;
        public DateTime day;
        public DateTime month;
        public DateTime year;

        public NextUpdate(DateTime date)
        {
            minute = date;
            hour = date;
            day = date;
            month = date;
            year = date;
        }
    }
}