﻿using AppFeatures.Models;
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

        private List<FlightLogInfo> FlightLogInfoItems
        {
            get
            {
                if (_flightLogInfoItems == null)
                {
                    _flightLogInfoItems = GetFlightLogInfoItems();
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
        public List<FlightLogInfo> GetFlightLogInfoItems()
        {
            return XMLSerializer.Deserialize<List<FlightLogInfo>>(XmlDataSourceFilePath);
        }

        /// <summary>
        /// Gets all records that were added to the flight log the past 7 dats.
        /// </summary>
        /// <returns>List with all flight log entries for the past week</returns>
        public List<FlightLogInfo> GetLastWeeksFlightLogData()
        {
            string searchTerm = "";
            DateTime startDate = DateTime.Now.AddDays(-6).Date;
            DateTime endDate = DateTime.Now;

            return FilterFlightLog(searchTerm, startDate, endDate);
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
        /// Filters data in the flight log based on a search term
        /// and a date interval
        /// </summary>
        /// <param name="searchTerm">Search term for the flight code.</param>
        /// <param name="startDate">Start date for the date interval.</param>
        /// <param name="endDate">End date for the date interval.</param>
        /// <returns>List with all flight log entries matching the method arguments.</returns>
        public List<FlightLogInfo> FilterFlightLog(
            string searchTerm,
            DateTime? startDate,
            DateTime? endDate)
        {
            if (searchTerm == null)
            {
                throw new ArgumentNullException("searchTerm", "searchTerm cannot be null.");
            }

            string searchTermLower = searchTerm.ToLower();

            IEnumerable<FlightLogInfo> query = GetLinqQueryForFilteringFlightsByFlightCode(searchTermLower);

            if (startDate != null)
            {
                // Update the date to only have the Date property to remove the time
                DateTime date = (DateTime)startDate;
                date = date.Date;

                query = 
                    from flightLogItem in query
                    where (flightLogItem.DateTime >= date)
                    select flightLogItem;
            }

            if (endDate != null)
            {
                // Since default time is 00:00:00 I'll add 23:59:59:999 to endDate so that
                // all flights on this date gets included in the search,
                // regardless of what time of day the event happened.
                TimeSpan timeToAdd = new TimeSpan(0, 23, 59, 59, 999);
                endDate += timeToAdd;

                query =
                    from flightLogItem in query
                    where (flightLogItem.DateTime <= endDate)
                    select flightLogItem;
            }

            return query.OrderByDescending(x => x.DateTime).ToList();
        }

        /// <summary>
        /// Gets a search query for filtering the flight log by flight code.
        /// </summary>
        /// <param name="searchTerm">Search term for the flight code.</param>
        /// <returns>A query that will filter the flight log based on the search term.</returns>
        private IEnumerable<FlightLogInfo> GetLinqQueryForFilteringFlightsByFlightCode(
            string searchTerm)
        {
            if (searchTerm == null)
            {
                throw new ArgumentNullException("searchTerm", "searchTerm cannot be null.");
            }

            string searchTermLower = searchTerm.ToLower();

            IEnumerable<FlightLogInfo> query = 
                from flightLigItem in FlightLogInfoItems
                where (flightLigItem.FlightCode.ToLower().Contains(searchTermLower))
                select flightLigItem;

            return query;
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
