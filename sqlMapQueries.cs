using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourglass
{
    public class sqlMapQueries
    {
        public MapData data { get; set; }

        public sqlMapQueries()
        {
        }

        public string GetTransitLine()
        {
            return data.routes[0].legs[0].steps[1].transit_details.line.short_name;
        }

        public long GetArrivalTime()
        {
            return data.routes[0].legs[0].arrival_time.value;
        }

        public long GetDepartureTime()
        {
            return data.routes[0].legs[0].departure_time.value;
        }

        public string GetIONDepartureTime()
        {
            return data.routes[0].legs[0].steps[1].transit_details.departure_time.text;
        }
    }
}
