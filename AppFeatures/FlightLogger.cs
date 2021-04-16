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
        private readonly List<FlightLogInfo> _flightLogInfoItems;

        /// <summary>
        /// List with FlightLogInfo objects that is used to store the flight log data
        /// that gets fetched from the storage.
        /// </summary>
        //private List<FlightLogInfo> FlightLogInfoItems { get; set; }

        /// <summary>
        /// List with FlightLogInfo objects that is based on FlightLogInfoItems.
        /// This one is used for binding to the GUI. It is this list that is modified
        /// when filtering.
        /// </summary>
        private List<FlightLogInfo> FilteredFlightLogInfoItems { get; set; }

        public FlightLogger()
        {
            _flightLogInfoItems = GetFlightLogInfoItems();
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
            //List<FlightLogInfo> flightLogInfoItems = XMLSerializer.Deserialize<List<FlightLogInfo>>(
            //    FilePaths.FlightLogFilePath);

            _flightLogInfoItems.Add(flightLogInfo);

            XMLSerializer.Serialize<List<FlightLogInfo>>(FilePaths.FlightLogFilePath, _flightLogInfoItems);
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
