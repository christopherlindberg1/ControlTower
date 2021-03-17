using AppFeatures.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Utility;

namespace AppFeatures
{
    public class FlightLogger
    {
        public void OnTakeOff(object source, TakeOffEventArgs e)
        {
            FlightLogInfo flightLogInfo = new FlightLogInfo()
            {
                FlightCode = e.FlightCode,
                Status = e.Status,
                DateTime = e.DateTime
            };

            AddFlightLogInfoItemToLog(flightLogInfo);
        }

        public void OnLanded(object source, LandEventArgs e)
        {
            FlightLogInfo flightLogInfo = new FlightLogInfo()
            {
                FlightCode = e.FlightCode,
                Status = e.Status,
                DateTime = e.DateTime
            };

            AddFlightLogInfoItemToLog(flightLogInfo);
        }

        private void AddFlightLogInfoItemToLog(FlightLogInfo flightLogInfo)
        {
            List<FlightLogInfo> flightLogInfoItems = XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePaths.SampleFlightLogFilePath);

            flightLogInfoItems.Add(flightLogInfo);

            XMLSerializer.Serialize<List<FlightLogInfo>>(FilePaths.SampleFlightLogFilePath, flightLogInfoItems);
        }

        public static List<FlightLogInfo> GetFlightLogInfoItems()
        {
            return XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePaths.SampleFlightLogFilePath);
        }
    }
}
