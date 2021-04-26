using DataAccess;
using DataAccess.Models;
using DataAccess.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AppFeatures.Tests
{
    public class FlightLogHandlerTests
    {
        [Fact]
        public void AddFlightLogInfoItemToList_AddingNull_ThrowsArgumentNullException()
        {
            // Arrange
            FlightLogHandler flightLogHandler = new FlightLogHandler(null);
            FlightLogInfo flightLogInfo = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => flightLogHandler.AddFlightLogInfoItemToList(flightLogInfo));
        }

        [Fact]
        public void AddFlightLogInfoItemToList_AddingLegitFlightLog_AddsToList()
        {
            // Arrange
            FlightLogHandler flightLogHandler = new FlightLogHandler(
                new XmlFlightLogger(FilePathsForTesting.PathForXmlFileWithData));

            int initialNrOfItems = flightLogHandler.FlightLogInfoItems.Count;
            int expectedAmountAfterTest = initialNrOfItems + 1;

            FlightLogInfo flightLogInfo = new FlightLogInfo
            {
                FlightCode = "SAS 555",
                Status = "Toof off",
                DateTime = new DateTime(2021, 1, 5, 15, 2, 6)
            };

            // Act
            flightLogHandler.AddFlightLogInfoItemToList(flightLogInfo);

            // Assert
            int listLength = flightLogHandler.FlightLogInfoItems.Count;

            Assert.Equal(
                expectedAmountAfterTest,
                flightLogHandler.FlightLogInfoItems.Count);

            Assert.Equal(
                flightLogInfo.FlightCode,
                flightLogHandler.FlightLogInfoItems[listLength - 1].FlightCode);
            Assert.Equal(
                flightLogInfo.Status,
                flightLogHandler.FlightLogInfoItems[listLength - 1].Status);
            Assert.Equal(
                flightLogInfo.DateTime,
                flightLogHandler.FlightLogInfoItems[listLength - 1].DateTime);
        }
    }
}
