using DataAccess;
using DataAccess.Models;
using DataAccess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures
{
    /// <summary>
    /// This part of the class FlightLogger contains methods that has been
    /// used when testing the application. These methods can be used when a developer
    /// wants to add more sample data.
    /// </summary>
    public partial class FlightLogHandler
    {
        /// <summary>
        /// Used to add bulk data to the log file so I can verifying that the app can
        /// handle a large number of records.
        /// 
        /// This method takes a few minutes to execute. Each time a record is added (I added 5400) 
        /// the entire log gets deserialized, then I add an object, and then I serialize.
        /// So the file is opened 10800 times.
        /// 
        /// The 5400 records corresponds to 27000 lines of XML and 880 kB of storage.
        /// The app can filter on all these records on multiple parameters without noticable lag.
        /// 
        /// In a real world app I would use multiple files (one for each month or week) or Sqlite 
        /// but I just used one file now for simplicity.
        /// </summary>
        public void AddBulkData()
        {
            AddManyFlights("SAS 794", "Took off", 200);
            AddManyFlights("SAS 794", "Landed", 200);
            AddManyFlights("CA 593", "Took off", 400);
            AddManyFlights("SAS 794", "Landed", 400);
            AddManyFlights("LH 69B", "Took off", 800);
            AddManyFlights("LH 69B", "Landed", 800);
            AddManyFlights("AA 7273K", "Took off", 700);
            AddManyFlights("AA 7273K", "Landed", 700);
            AddManyFlights("KLM 666", "Took off", 600);
            AddManyFlights("KLM 666", "Landed", 600);
        }

        /// <summary>
        /// Used when testing the log functionality.
        /// Not used in "production."
        /// </summary>
        private void AddManyFlights(string flightCode, string status, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                FlightLogInfo flightLogInfo = new FlightLogInfo()
                {
                    FlightCode = flightCode,
                    Status = status,
                    DateTime = DateTime.Now
                };

                AddFlightLogInfoItemToLog(flightLogInfo);
            }
        }

        /// <summary>
        /// Used when testing the log functionality.
        /// Not used in "production."
        /// </summary>
        public void ClearLog()
        {
            XMLSerializer.Serialize<List<FlightLogInfo>>(
                TextFileFlightLogger.FilePath, new List<FlightLogInfo>());
        }
    }
}
