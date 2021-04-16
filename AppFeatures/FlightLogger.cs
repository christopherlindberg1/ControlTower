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
    /// <summary>
    /// Class used for logging different actions related to flights in some type
    /// of a permanent storage.
    /// </summary>
    public partial class FlightLogger
    {
        /// <summary>
        /// List with FlightLogInfo objects that is used to store the flight log data
        /// that gets fetched from the storage.
        /// </summary>
        public List<FlightLogInfo> FlightLogInfoItems { get; set; }

        /// <summary>
        /// List with FlightLogInfo objects that is based on FlightLogInfoItems.
        /// This one is used for binding to the GUI. It is this list that is modified
        /// when filtering.
        /// </summary>
        private List<FlightLogInfo> FilteredFlightLogInfoItems { get; set; }


        /// <summary>
        /// Used to add bulk data to the log file so I can verifying that the app can
        /// handle a large number of records.
        /// 
        /// This method takes a few minutes to execute. Each time a record is added (I added 5400) 
        /// the entire log gets deserialized, then I add an object, and then I serialize.
        /// So the file is opened 10800 times.
        /// 
        /// The 5400 records corresponds to 27000 lines of XML and 880 kB of storage.s
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
                FilePaths.FlightLogFilePath, new List<FlightLogInfo>());
        }

        /// <summary>
        /// Event handler/listener for when an airplane has taken off.
        /// </summary>
        /// <param name="source">Object that triggered the event</param>
        /// <param name="e">TakeOffEventArgs object containing event data</param>
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

        /// <summary>
        /// Event handler/listener for when an airplane has landed
        /// </summary>
        /// <param name="source">Object that triggered the event</param>
        /// <param name="e">LandEventArgs object containing event data</param>
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

        /// <summary>
        /// Adds a FlightLogItem to the log file.
        /// </summary>
        /// <param name="flightLogInfo">FlightLogInfo object</param>
        private void AddFlightLogInfoItemToLog(FlightLogInfo flightLogInfo)
        {
            List<FlightLogInfo> flightLogInfoItems = XMLSerializer.Deserialize<List<FlightLogInfo>>(
                FilePaths.FlightLogFilePath);

            flightLogInfoItems.Add(flightLogInfo);

            XMLSerializer.Serialize<List<FlightLogInfo>>(FilePaths.FlightLogFilePath, flightLogInfoItems);
        }

        /// <summary>
        /// Gets the entire flight log.
        /// </summary>
        /// <returns>List with FlightLogInfo items</returns>
        public List<FlightLogInfo> GetFlightLogInfoItems()
        {
            return XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePaths.FlightLogFilePath);
        }
    }
}
