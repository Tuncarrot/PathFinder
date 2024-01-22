using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourglass
{
    public class timeManager
    {
        public DateTime ConvertUnixToDateTime(long time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(time).ToLocalTime();
        }

        public long GetWorkoutTime(DateTime ArriveGym, DateTime CatchBusBackTime)
        {
            // 3 min to change, 3 min head start to ion
            TimeSpan ts = CatchBusBackTime.AddMinutes(-6).Subtract(ArriveGym);
            return ts.Minutes;
        }

        public DateTime GetChangeTime(DateTime LeaveGym)
        {
            return LeaveGym.AddMinutes(-6);
        }

        public string StripDateTime(DateTime datetime)
        {
            return datetime.ToString("hh:mm tt");
        }
    }
}
