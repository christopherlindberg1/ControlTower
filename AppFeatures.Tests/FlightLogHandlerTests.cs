using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFeatures;
using AppFeatures.Models;
using Xunit;

namespace AppFeatures.Tests
{
    public class FlightLogHandlerTests
    {
        /// <summary>
        /// Testing FlightLogHandler.FilterFlightLog() with an empty string as
        /// the search term as well as null as the start- and end date.
        /// The filter method not filter out any entries.
        /// </summary>
        [Fact]
        public void FilterFlightLog_NoSearchParameters_ReturnsFullList()
        {
            // Arrange
            FlightLogHandler flightLogHandler = new FlightLogHandler();
            List<FlightLogInfo> flightLog = FlightLogHandlerTestData.SampleFlightLog;

            // Act
            List<FlightLogInfo> filteredFlightLog = flightLogHandler.FilterFlightLog(
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
        public void FilterFlightLog_OnlyProvidingSearchTerm_FiltersOutMismatches(string searchTerm, int expectedAmount)
        {
            // Arrange
            FlightLogHandler flightLogHandler = new FlightLogHandler();
            List<FlightLogInfo> flightLog = FlightLogHandlerTestData.SampleFlightLog;

            // Act
            List<FlightLogInfo> filteredFlightLog = flightLogHandler.FilterFlightLog(
                flightLog, searchTerm, null, null);

            // Assert
            Assert.Equal(expectedAmount, filteredFlightLog.Count);

            // Checking that every item has a FlightCode matching the search term
            foreach (FlightLogInfo item in filteredFlightLog)
            {
                Assert.Contains(searchTerm.ToLower(), item.FlightCode.ToLower());
            }
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
            nameof(FlightLogHandlerTestData.ArgumentsForFilteringWithDateTimes),
            MemberType = typeof(FlightLogHandlerTestData))]
        public void FilterFlightLog_OnlyProvidingDateTimes_FiltersOutMismatches(
            DateTime? startDate,
            DateTime? endDate,
            int expectedAmount)
        {
            // Arrange
            FlightLogHandler flightLogHandler = new FlightLogHandler();
            List<FlightLogInfo> flightLog = FlightLogHandlerTestData.SampleFlightLog;

            // Act
            List<FlightLogInfo> filteredFlightLog = flightLogHandler.FilterFlightLog(
                flightLog, "", startDate, endDate);

            // Assert
            Assert.Equal(expectedAmount, filteredFlightLog.Count);
        }

        [Theory]
        [MemberData(
            nameof(FlightLogHandlerTestData.ArgumentsForFilteringWithSearchTermAndDateTimes),
            MemberType = typeof(FlightLogHandlerTestData))]
        public void FilterFlightLog_ProvidingSearchTermAndDateTimes_FiltersOutMismatches(
            string searchTerm,
            DateTime? startDate,
            DateTime? endDate,
            int expectedAmount)
        {
            // Arrange
            FlightLogHandler flightLogHandler = new FlightLogHandler();
            List<FlightLogInfo> flightLog = FlightLogHandlerTestData.SampleFlightLog;

            // Act
            List<FlightLogInfo> filteredFlightLog = flightLogHandler.FilterFlightLog(
                flightLog, searchTerm, startDate, endDate);

            // Assert
            Assert.Equal(expectedAmount, filteredFlightLog.Count);

            // Checking that every item has a FlightCode matching the search term
            foreach (FlightLogInfo item in filteredFlightLog)
            {
                Assert.Contains(searchTerm.ToLower(), item.FlightCode.ToLower());
            }
        }
    }
}
