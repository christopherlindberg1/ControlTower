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

            yield return new FlightLogInfo
            {
                FlightCode = "SAS 794",
                Status = "Landed",
                DateTime = new DateTime(2020, 5, 23, 18, 12, 34)
            };
            
            yield return new FlightLogInfo
            {
                FlightCode = "DLH 812",
                Status = "Took off",
                DateTime = new DateTime(2021, 4, 15, 9, 52, 15)
            };
            
            yield return new FlightLogInfo
            {
                FlightCode = "DLH 812",
                Status = "Landed",
                DateTime = new DateTime(2021, 4, 15, 15, 2, 9)
            };
            
            yield return new FlightLogInfo
            {
                FlightCode = "SAS 794",
                Status = "Took off",
                DateTime = new DateTime(2021, 4, 15, 16, 52, 15)
            };
            
            yield return new FlightLogInfo
            {
                FlightCode = "SAS 794",
                Status = "Landed",
                DateTime = new DateTime(2021, 4, 15, 18, 18, 0)
            };
            
            yield return new FlightLogInfo
            {
                FlightCode = "KLM 666",
                Status = "Took off",
                DateTime = new DateTime(2021, 4, 18, 23, 7, 8)
            };
        }
    }
}
