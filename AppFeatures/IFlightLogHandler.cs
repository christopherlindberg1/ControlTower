using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFeatures.FlightActionsEventArgs;

namespace AppFeatures
{
    public interface IFlightLogHandler
    {
        IFlightLogger FlightLogger { get; }

        List<FlightLogInfo> FlightLogInfoItems { get; }

        int TotalNumberOfFlightLogRecords { get; }

        void AddFlightLogInfoItemToList(FlightLogInfo flightLogInfo);

        void AddFlightLogInfoItemToLog(FlightLogInfo flightLogInfo);

        void OnTakeOff(object source, TakeOffEventArgs e);

        void OnLanded(object source, LandEventArgs e);
    }
}
