using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using DataAccess.Utility;

namespace AppFeatures
{
    /// <summary>
    /// Class used for logging different actions related to flights in some type
    /// of a permanent storage.
    /// </summary>
    public partial class FlightLogHandler
    {
        private readonly ITextFileFlightLogger _textFileFlightLogger;
        private List<FlightLogInfo> _flightLogInfoItems;




        // ===================== Properties ===================== //

        public ITextFileFlightLogger TextFileFlightLogger { get => _textFileFlightLogger; }

        public List<FlightLogInfo> FlightLogInfoItems
        {
            get
            {
                if (_flightLogInfoItems == null)
                {
                    _flightLogInfoItems = TextFileFlightLogger.GetLog();
                }

                return _flightLogInfoItems;
            }
        }

        public int TotalNumberOfFlightLogRecords { get => _flightLogInfoItems.Count; }




        // ===================== Methods ===================== //

        public FlightLogHandler(ITextFileFlightLogger logger)
        {
            _textFileFlightLogger = logger;
        }

        /// <summary>
        /// Adds a FlightLogItem to the list with FlightLogInfo objects.
        /// </summary>
        /// <param name="flightLogInfo">FlightLogInfo object</param>
        private void AddFlightLogInfoItemToList(FlightLogInfo flightLogInfo)
        {
            if (flightLogInfo == null)
            {
                throw new ArgumentNullException("flightLogInfo", "flightLogInfo cannot be null.");
            }

            FlightLogInfoItems.Add(flightLogInfo);
        }

        /// <summary>
        /// Adds a FlightLogItem to the log file.
        /// </summary>
        /// <param name="flightLogInfo">FlightLogInfo object</param>
        private void AddFlightLogInfoItemToLog(FlightLogInfo flightLogInfo)
        {
            if (flightLogInfo == null)
            {
                throw new ArgumentNullException("flightLogInfo", "flightLogInfo cannot be null.");
            }

            TextFileFlightLogger.SaveEntryInLog(flightLogInfo);
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

            AddFlightLogInfoItemToList(flightLogInfo);
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
            
            AddFlightLogInfoItemToList(flightLogInfo);
            AddFlightLogInfoItemToLog(flightLogInfo);
        }
    }
}
