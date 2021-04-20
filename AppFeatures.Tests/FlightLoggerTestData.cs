using AppFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures.Tests
{
    public class FlightLoggerTestData
    {
        private static List<FlightLogInfo> _sampleFullFlightLog = new List<FlightLogInfo>
        {
            new FlightLogInfo
            {
                FlightCode = "SAS 794",
                Status = "Took off",
                DateTime = new DateTime(2020, 1, 1, 12, 45, 45)
            },
        };

        public static List<FlightLogInfo> SampleFullFlightLog { get => _sampleFullFlightLog; }
        
    }
}
