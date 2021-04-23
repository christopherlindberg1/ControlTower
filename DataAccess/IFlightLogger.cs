using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Interface specifying which members any type of flight logger should have.
    /// </summary>
    public interface IFlightLogger
    {
        List<FlightLogInfo> GetLog();

        void SaveEntryInLog(FlightLogInfo flightLogEntry);
    }
}
