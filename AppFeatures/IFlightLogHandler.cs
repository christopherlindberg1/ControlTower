using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures
{
    public interface IFlightLogHandler
    {
        List<FlightLogInfo> FilterFlightLog(
            List<FlightLogInfo> flightLogInfoItems,
            string searchTerm,
            DateTime? startDate,
            DateTime? endDate);
    }
}
