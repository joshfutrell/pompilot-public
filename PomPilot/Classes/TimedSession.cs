using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomPilot.Classes
{
    public abstract class TimedSession
    {
        public TimeSpan TargetDuration { get; private set; }
        public Nullable<DateTime> StartTime { get; private set; } = null;
        public Nullable<DateTime> EndTime { get; private set; } = null;
        public TimeSpan PausedDuration { get; private set; }
            
        public bool Start(TimeSpan duration)
        {
            StartTime = DateTime.Now;
            TargetDuration = duration;
            return false;
        }

        public TimeSpan TimeRemaining(DateTime now)
        {
            DateTime targetTime = Convert.ToDateTime(StartTime).Add(TargetDuration);
            return (targetTime > now) ? now - targetTime : new TimeSpan(0, 0, 0, 0);
        }
    }
}
