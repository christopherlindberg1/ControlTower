using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFeatures.Models;

namespace AppFeature.Tests
{
    public class FlightLoggerTestsDataGenerator
    {
        public static IEnumerable<object> GetSampleFlightLogInfoData()
        {
            yield return new FlightLogInfo
            {
                FlightCode = "SAS 794",
                Status = "Took off",
                DateTime = new DateTime(2020, 5, 23, 14, 52, 15)
            };
        }
    }
}
