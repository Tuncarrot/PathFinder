using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourglass
{
    public class stringConstructor
    {
        public stringConstructor()
        {

        }

        public string CreateTextString(string todayDate, string leavingHouse, string arrivingGym, string arrivingHouse, string IONLeavesGym, long workoutTime, string changingTime)
        {
            string message = "";

            message += "📅" + todayDate + " Gym Plan\n";

            message += "🏠 Leave Home at " + leavingHouse + "\n";
            message += "⏰ Arrive at gym at " + arrivingGym + "\n";
            message += "🏋 " + workoutTime.ToString() + " minutes exercise\n";
            message += "👟 Change at " + changingTime + "\n";
            message += "🚆 ION will leave at " + IONLeavesGym + "\n";
            message += "🏠 Arrive Home at " + arrivingHouse + "\n";

            return message;
        }
    }
}
