using AppFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures.Tests
{
    public class FlightLogHandlerTestData
    {
        private static FlightLogInfo _sampleFlightLogInfoItem = new FlightLogInfo
        {
            FlightCode = "SAS 333",
            Status = "Landed",
            DateTime = new DateTime(2021, 4, 20, 14, 15, 15)
        };

        private static List<FlightLogInfo> _sampleFlightLog = new List<FlightLogInfo>
        {
            new FlightLogInfo
            {
                FlightCode = "SAS 794",
                Status = "Took off",
                DateTime = new DateTime(2020, 1, 1, 12, 45, 45)
            },
            // Add more items...
        };

        public static List<FlightLogInfo> SampleFlightLog { get => _sampleFlightLog; }

        public static FlightLogInfo SampleFlightLogInfoItem { get => _sampleFlightLogInfoItem; }
    }
}
