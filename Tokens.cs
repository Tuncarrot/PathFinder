using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourglass
{
    public class Tokens
    {
        public string apiKey = Environment.GetEnvironmentVariable("GYM_PLANNER_APIKEY");
        public string home = Environment.GetEnvironmentVariable("GYM_PLANNER_HOME");
        public string gym = Environment.GetEnvironmentVariable("GYM_PLANNER_GYM");
        public string requiredTransitLine = Environment.GetEnvironmentVariable("GYM_PLANNER_TRANSIT");

        public string accountSid = Environment.GetEnvironmentVariable("GYM_PLANNER_accountSid");
        public string authToken = Environment.GetEnvironmentVariable("GYM_PLANNER_AUTHTOKEN");

        public string myTwilioNum = Environment.GetEnvironmentVariable("GYM_PLANNER_TWILIONUM");

        public readonly List<string> numbers = new List<string>
        {
            Environment.GetEnvironmentVariable("GYM_PLANNER_NUM_1"),
            Environment.GetEnvironmentVariable("GYM_PLANNER_NUM_2")
        };

    }
}
