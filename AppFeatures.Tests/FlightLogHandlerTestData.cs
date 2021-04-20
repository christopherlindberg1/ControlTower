using AppFeatures.Models;
using System;
using System.Collections;
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
            new FlightLogInfo
            {
                FlightCode = "SAS 794",
                Status = "Landed",
                DateTime = new DateTime(2020, 1, 1, 16, 45, 45)
            },
            new FlightLogInfo
            {
                FlightCode = "DLH 512",
                Status = "Took off",
                DateTime = new DateTime(2021, 2, 2, 10, 30, 15)
            },
            new FlightLogInfo
            {
                FlightCode = "DLH 512",
                Status = "Landed",
                DateTime = new DateTime(2021, 2, 2, 12, 55, 7)
            },
            new FlightLogInfo
            {
                FlightCode = "SAS 111",
                Status = "Took off",
                DateTime = new DateTime(2021, 4, 20, 8, 7, 0)
            },
            new FlightLogInfo
            {
                FlightCode = "RYS 895",
                Status = "Took off",
                DateTime = new DateTime(2021, 4, 20, 11, 0, 6)
            },
        };

        public static List<FlightLogInfo> SampleFlightLog { get => _sampleFlightLog; }

        public static FlightLogInfo SampleFlightLogInfoItem { get => _sampleFlightLogInfoItem; }

        public static IEnumerable<object[]> ArgumentsForFilteringWithDateTimes()
        {
            yield return new object[]
            {
                new DateTime(2020, 1, 1 ),
                new DateTime(2021, 4, 20),
                6
            };
        }
    }
}
