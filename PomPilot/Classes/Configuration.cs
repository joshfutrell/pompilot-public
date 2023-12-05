using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomPilot.Classes
{
    public class Configuration
    {
        // TODO - move defaults to a config json
        public TimeSpan DefaultPomodoroDuration { get; private set; } = new TimeSpan(0, 0, 25, 0, 0);
        public TimeSpan CustomPomodoroDuration { get; set; }

    }
}
