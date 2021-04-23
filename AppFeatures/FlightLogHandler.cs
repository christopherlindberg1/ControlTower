using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures
{
    /// <summary>
    /// Class used to manipulate flight log data held in RAM.
    /// Performs operations such as filtering the log.
    /// </summary>
    public class FlightLogHandler : IFlightLogHandler
    {
        /// <summary>
        /// Filters data in the flight log based on a search term
        /// and a date interval
        /// </summary>
        /// <param name="searchTerm">Search term for the flight code.</param>
        /// <param name="startDate">Start date for the date interval.</param>
        /// <param name="endDate">End date for the date interval.</param>
        /// <returns>List with all flight log entries matching the method arguments.</returns>
        public List<FlightLogInfo> FilterFlightLog(
            List<FlightLogInfo> flightLogInfoItems,
            string searchTerm,
            DateTime? startDate,
            DateTime? endDate)
        {
            if (searchTerm == null)
            {
                throw new ArgumentNullException("searchTerm", "searchTerm cannot be null.");
            }

            string searchTermLower = searchTerm.ToLower();

            IEnumerable<FlightLogInfo> query =
                GetLinqQueryForFilteringFlightsByFlightCode(flightLogInfoItems, searchTermLower);

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
                // Set time to 00:00:00 and then add 23:59:59:999 in order to cover
                // all entries on that date.
                DateTime date = (DateTime)endDate;
                date = date.Date; // Sets time to 00:00:00
                TimeSpan timeToAdd = new TimeSpan(0, 23, 59, 59, 999);
                date += timeToAdd;

                query =
                    from flightLogItem in query
                    where (flightLogItem.DateTime <= date)
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
            List<FlightLogInfo> flightLogInfoItems,
            string searchTerm)
        {
            if (searchTerm == null)
            {
                throw new ArgumentNullException("searchTerm", "searchTerm cannot be null.");
            }

            string searchTermLower = searchTerm.ToLower();

            IEnumerable<FlightLogInfo> query =
                from flightLigItem in flightLogInfoItems
                where (flightLigItem.FlightCode.ToLower().Contains(searchTermLower))
                select flightLigItem;

            return query;
        }
    }
}
