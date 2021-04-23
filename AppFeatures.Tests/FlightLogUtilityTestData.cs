using DataAccess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures.Tests
{
    public class FlightLogUtilityTestData
    {
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

        public static IEnumerable<object[]> ArgumentsForFilteringWithDateTimes()
        {
            // Covers the entire time interval, expects entire log (6 entries).
            yield return new object[]
            {
                new DateTime(2020, 1, 1),
                new DateTime(2021, 4, 20),
                6
            };

            // Start- and end date before first entry, expects 0 entries.
            yield return new object[]
            {
                new DateTime(2019, 12, 31),
                new DateTime(2019, 12, 31),
                0
            };

            // Time span covers the first date a flight was logged, expects 2 entries.
            yield return new object[]
            {
                new DateTime(2019, 12, 31),
                new DateTime(2020, 1, 1),
                2
            };

            // Time span starts and ends on the first date a flight was logged,
            // expects 2 entries.
            yield return new object[]
            {
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 1),
                2
            };

            // Time span starts and ends when a flight in the middle
            // of the log was logged. Expects 2 entries.
            yield return new object[]
            {
                new DateTime(2021, 2, 2),
                new DateTime(2021, 2, 2),
                2
            };

            // Time span stops right before more flights were logged,
            // expects 2 entries.
            yield return new object[]
            {
                new DateTime(2021, 2, 2),
                new DateTime(2021, 4, 19),
                2
            };

            // Time span stops at the last logged time. Expects to include the last
            // entries, expects 4 entries.
            yield return new object[]
            {
                new DateTime(2021, 2, 2),
                new DateTime(2021, 4, 20),
                4
            };

            // Start date includes time of day that is after a flight was logged.
            // This flight should be included anyway since it should include all 
            // logs on a particular date, and not look at the time.
            yield return new object[]
            {
                new DateTime(2020, 1, 1, 23, 59, 59),
                new DateTime(2020, 1, 1, 23, 59, 59),
                2
            };

            // End date includes time of day that is before a flight was logged.
            // This flight should be included anyway since it should include all 
            // logs on a particular date, and not look at the time.
            yield return new object[]
            {
                new DateTime(2021, 4, 20, 0, 0, 0),
                new DateTime(2021, 4, 20, 0, 0, 0),
                2
            };
            
            // Start date is null, end date is declared. Should include every entry
            // up to, and including, the end date. Expects 4 entries.
            yield return new object[]
            {
                null,
                new DateTime(2021, 2, 2),
                4
            };

            // Start date is declared, end date is null. Should include every entry
            // from the start date up to, and including, the rest of the last
            // entry in the log. Expects 4 entries.
            yield return new object[]
            {
                new DateTime(2021, 2, 2),
                null,
                4
            };

            // Start- and end date is null. Should include every entry
            // in the log. Expects 6 entries.
            yield return new object[]
            {
                null,
                null,
                6
            };
        }

        public static IEnumerable<object[]> ArgumentsForFilteringWithSearchTermAndDateTimes()
        {
            // Filters for 'SAS' in the entire time interval. Expects 3 entries.
            yield return new object[]
            {
                "SAS",
                new DateTime(2020, 1, 1),
                new DateTime(2021, 4, 20),
                3
            };

            // Filters for 'S' in the entire time interval. Expects 3 entries.
            yield return new object[]
            {
                "S",
                new DateTime(2020, 1, 1),
                new DateTime(2021, 4, 20),
                4
            };

            // Filters for 'SAS' in a specific time interval. Should exclude 1
            // entry with SAS. Expects 2 entries.
            yield return new object[]
            {
                "SAS",
                new DateTime(2020, 1, 1),
                new DateTime(2021, 4, 19),
                2
            };

            // Filters for 'DHL' in a specific time interval. Shoudl exclude 2
            // entries with DLH. Expects 0 entries.
            yield return new object[]
            {
                "DLH",
                new DateTime(2020, 1, 1),
                new DateTime(2020, 12, 31),
                0
            };
        }
    }
}
