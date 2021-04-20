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
        private readonly string _xmlDataSourceFilePath;
        private List<FlightLogInfo> _flightLogInfoItems;




        // ===================== Properties ===================== //

        public string XmlDataSourceFilePath { get => _xmlDataSourceFilePath; }

        public List<FlightLogInfo> FlightLogInfoItems
        {
            get
            {
                if (_flightLogInfoItems == null)
                {
                    _flightLogInfoItems = GetFlightLogInfoItemsFromStorage();
                }

                return _flightLogInfoItems;
            }
        }

        public int TotalNumberOfFlightLogRecords { get => _flightLogInfoItems.Count; }




        // ===================== Methods ===================== //

        public FlightLogger(string storageFilePath)
        {
            _xmlDataSourceFilePath = storageFilePath;
        }

        /// <summary>
        /// Gets the entire flight log.
        /// </summary>
        /// <returns>List with FlightLogInfo items</returns>
        private List<FlightLogInfo> GetFlightLogInfoItemsFromStorage()
        {
            return XMLSerializer.Deserialize<List<FlightLogInfo>>(XmlDataSourceFilePath);
        }

        /// <summary>
        /// Adds a FlightLogItem to the log file.
        /// </summary>
        /// <param name="flightLogInfo">FlightLogInfo object</param>
        private void AddFlightLogInfoItemToLog(FlightLogInfo flightLogInfo)
        {
            FlightLogInfoItems.Add(flightLogInfo);

            XMLSerializer.Serialize<List<FlightLogInfo>>(XmlDataSourceFilePath, FlightLogInfoItems);
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
    }
}
