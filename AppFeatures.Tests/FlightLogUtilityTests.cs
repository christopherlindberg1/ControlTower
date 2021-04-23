using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFeatures;
using DataAccess.Models;
using Xunit;

namespace AppFeatures.Tests
{
    public class FlightLogUtilityTests
    {
        /// <summary>
        /// Class providing utility methods for the data being tested
        /// in FlightLogHandlerTests.
        /// </summary>
        public class Utility
        {
            /// <summary>
            /// Counts the number of items in a collection that left when having
            /// filtered the collection based on the provided filter arguments.
            /// </summary>
            /// <param name="flightLog">List with FlightLogInfo objects.</param>
            /// <param name="searchTerm">Search term to check for in each FlightCode.</param>
            /// <param name="startDate">Start date of the time interval to look in.</param>
            /// <param name="endDate">End date of the time interval to look in.</param>
            /// <returns>The amount of items that are left after having filtered The list.</returns>
            public static int CountItemsMatchingSearchParameters(
                List<FlightLogInfo> flightLog,
                string searchTerm,
                DateTime? startDate,
                DateTime? endDate)
            {
                IEnumerable<FlightLogInfo> query =
                    GetQueryForFilteringBySearchTerm(flightLog, searchTerm);

                if (startDate != null)
                {
                    query = GetQueryForFilteringByStartDate(query, (DateTime)startDate);
                }

                if (endDate != null)
                {
                    query = GetQueryForFilteringByEndDate(query, (DateTime)endDate);
                }

                return query.ToList().Count;
            }

            /// <summary>
            /// Gets LINQ query for filtering a given collection or existing query
            /// by a searchTerm.
            /// </summary>
            /// <param name="itemToQuery">IEnumberable of type FlightLogInfo to filter.</param>
            /// <param name="searchTerm">Search term to check for in each FlightCode.</param>
            /// <returns>LINQ query with an added segment that filters by searchTerm.</returns>
            private static IEnumerable<FlightLogInfo> GetQueryForFilteringBySearchTerm(
                IEnumerable<FlightLogInfo> itemToQuery,
                string searchTerm)
            {
                return from item in itemToQuery
                       where item.FlightCode.ToLower().Contains(searchTerm.ToLower())
                       select item;
            }

            /// <summary>
            /// Gets LINQ query for filtering a given collection or existing query
            /// by a start date.
            /// </summary>
            /// <param name="itemToQuery">IEnumberable of type FlightLogInfo to filter.</param>
            /// <param name="startDate">Start date of the time interval to include.</param>
            /// <returns>LINQ query with an added segment that filters by startDate.</returns>
            private static IEnumerable<FlightLogInfo> GetQueryForFilteringByStartDate(
                IEnumerable<FlightLogInfo> itemToQuery,
                DateTime startDate)
            {

                return from item in itemToQuery
                       where item.DateTime >= startDate.Date
                       select item;
            }

            /// <summary>
            /// Gets LINQ query for filtering a given collection or existing query
            /// by an end date.
            /// </summary>
            /// <param name="itemToQuery">IEnumberable of type FlightLogInfo to filter.</param>
            /// <param name="endDate">End date of the time interval to include.</param>
            /// <returns>LINQ query with an added segment that filters by endDate.</returns>
            private static IEnumerable<FlightLogInfo> GetQueryForFilteringByEndDate(
                IEnumerable<FlightLogInfo> itemToQuery,
                DateTime endDate)
            {
                endDate = endDate.Date;
                TimeSpan timeToAdd = new TimeSpan(0, 23, 59, 59, 999);
                endDate += timeToAdd;

                return from item in itemToQuery
                       where item.DateTime <= endDate
                       select item;
            }
        }

        /// <summary>
        /// Testing FlightLogHandler.FilterFlightLog() with an empty string as
        /// the search term as well as null as the start- and end date.
        /// The filter method not filter out any entries.
        /// </summary>
        [Fact]
        public void FilterFlightLog_NoSearchParameters_ReturnsFullList()
        {
            // Arrange
            FlightLogUtility flightLogUtility = new FlightLogUtility();
            List<FlightLogInfo> flightLog = FlightLogUtilityTestData.SampleFlightLog;

            // Act
            List<FlightLogInfo> filteredFlightLog = flightLogUtility.FilterFlightLog(
                flightLog, "", null, null);

            // Assert
            Assert.Equal(flightLog.Count, filteredFlightLog.Count);
        }

        /// <summary>
        /// Testsing FlightLogHandler.FilterFlightLog() with different
        /// search terms for the flightCode parameter in the filter method.
        /// startDate and endDate is set to null in all cases in order to not filter
        /// for a specific time span.
        /// </summary>
        /// <param name="searchTerm">The search term we want to see if it is contained in the flight code.</param>
        /// <param name="expectedAmount">The amount of entries that has a flight code containing the search term.</param>
        [Theory]
        [InlineData("SAS", 3)]
        [InlineData("sas", 3)]
        [InlineData("s", 4)]
        [InlineData("", 6)]
        [InlineData(" ", 6)]
        [InlineData("H ", 2)]
        [InlineData("1", 3)]
        [InlineData("11", 1)]
        [InlineData(" 11", 1)]
        public void FilterFlightLog_OnlyProvidingSearchTerm_FiltersOutMismatches(
            string searchTerm,
            int expectedAmount)
        {
            // Arrange
            FlightLogUtility flightLogUtility = new FlightLogUtility();
            List<FlightLogInfo> flightLog = FlightLogUtilityTestData.SampleFlightLog;
            int nrOfItemsMatchingSearchParameters = Utility.CountItemsMatchingSearchParameters(
                flightLog, searchTerm, null, null);

            // Act
            List<FlightLogInfo> filteredFlightLog = flightLogUtility.FilterFlightLog(
                flightLog, searchTerm, null, null);

            // Assert
            Assert.Equal(expectedAmount, filteredFlightLog.Count);
            Assert.Equal(nrOfItemsMatchingSearchParameters, filteredFlightLog.Count);
        }

        /// <summary>
        /// Testing FlightLogHandler.FilterFlightLog() with different
        /// conbinations of start- and end dates for the time interval. An empty string
        /// is used in all tests, which is why it is not passed in as an argument.
        /// </summary>
        /// <param name="startDate">Start date for the time interval.</param>
        /// <param name="endDate">end date for the time interval.</param>
        /// <param name="expectedAmount">The amount of entries that are in the interval.</param>
        [Theory]
        [MemberData(
            nameof(FlightLogUtilityTestData.ArgumentsForFilteringWithDateTimes),
            MemberType = typeof(FlightLogUtilityTestData))]
        public void FilterFlightLog_OnlyProvidingDateTimes_FiltersOutMismatches(
            DateTime? startDate,
            DateTime? endDate,
            int expectedAmount)
        {
            // Arrange
            FlightLogUtility flightLogUtility = new FlightLogUtility();
            List<FlightLogInfo> flightLog = FlightLogUtilityTestData.SampleFlightLog;
            int nrOfItemsMatchingSearchParameters = Utility.CountItemsMatchingSearchParameters(
                flightLog, "", startDate, endDate);

            // Act
            List<FlightLogInfo> filteredFlightLog = flightLogUtility.FilterFlightLog(
                flightLog, "", startDate, endDate);

            // Assert
            Assert.Equal(expectedAmount, filteredFlightLog.Count);
            Assert.Equal(nrOfItemsMatchingSearchParameters, filteredFlightLog.Count);
        }

        [Theory]
        [MemberData(
            nameof(FlightLogUtilityTestData.ArgumentsForFilteringWithSearchTermAndDateTimes),
            MemberType = typeof(FlightLogUtilityTestData))]
        public void FilterFlightLog_ProvidingSearchTermAndDateTimes_FiltersOutMismatches(
            string searchTerm,
            DateTime? startDate,
            DateTime? endDate,
            int expectedAmount)
        {
            // Arrange
            FlightLogUtility flightLogUtility = new FlightLogUtility();
            List<FlightLogInfo> flightLog = FlightLogUtilityTestData.SampleFlightLog;
            int nrOfItemsMatchingSearchParameters = Utility.CountItemsMatchingSearchParameters(
                flightLog, searchTerm, startDate, endDate);

            // Act
            List<FlightLogInfo> filteredFlightLog = flightLogUtility.FilterFlightLog(
                flightLog, searchTerm, startDate, endDate);

            // Assert
            Assert.Equal(expectedAmount, filteredFlightLog.Count);
            Assert.Equal(nrOfItemsMatchingSearchParameters, filteredFlightLog.Count);
        }
    }
}
