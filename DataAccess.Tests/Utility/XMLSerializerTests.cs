using AppFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Utility;
using Xunit;
using AppFeatures.Models;

namespace DataAccess.Tests.Utility
{
    public class XMLSerializerTests
    {
        [Fact]
        public void Deserialize_ValidFilePathWithMatchingDataType_Works()
        {
            // Act
            List<FlightLogInfo> flightLog = 
                XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePathsForTesting.PathForXmlFileWithData);

            // Assert
            // Checking that we get the correct amount of objects
            Assert.Equal(4, flightLog.Count);
            // Checking values of the first entry
            Assert.Equal("SAS 794", flightLog[0].FlightCode);
            Assert.Equal("Took off", flightLog[0].Status);
            Assert.Equal(new DateTime(2020, 5, 23, 13, 8, 17), flightLog[0].DateTime);
            // Checking values of the last entry
            Assert.Equal("DLH 812", flightLog[3].FlightCode);
            Assert.Equal("Landed", flightLog[3].Status);
            Assert.Equal(new DateTime(2021, 4, 15, 17, 8, 17), flightLog[3].DateTime);
        }

        [Fact]
        public void Deserialize_ValidFilePathWithMismatchingDataType_ThrowsInvalidOperationException()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(
                () => XMLSerializer.Deserialize<FlightLogInfo>(FilePathsForTesting.PathForXmlFileWithData));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Deserialize_InvalidFilePath_ThrowsArgumentNullException(string filePath)
        {
            // Assert
            Assert.Throws<ArgumentNullException>(
                () => XMLSerializer.Deserialize<List<FlightLogInfo>>(filePath));
        }

        [Fact]
        public void Serialize_SaveCollectionOfItems_Works()
        {
            // Arrange
            List<FlightLogInfo> flightLogToIsert = new List<FlightLogInfo>
            {
                new FlightLogInfo
                {
                    FlightCode = "SAS 794",
                    Status = "Took off",
                    DateTime = new DateTime(2021, 04, 18, 12, 00, 00)
                }
            };

            // Act
            XMLSerializer.Serialize<List<FlightLogInfo>>(
                FilePathsForTesting.PathForXmlFileUsedForInsert, flightLogToIsert);

            List<FlightLogInfo> flightLogFromFile =
                XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePathsForTesting.PathForXmlFileUsedForInsert);

            // Assert
            Assert.Single(flightLogToIsert);

            Assert.Equal(flightLogToIsert[0].FlightCode, flightLogFromFile[0].FlightCode);
            Assert.Equal(flightLogToIsert[0].Status, flightLogFromFile[0].Status);
            Assert.Equal(flightLogToIsert[0].DateTime, flightLogFromFile[0].DateTime);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Serialize_InvalidFilePath_ThrowsArgumentNullException(string filePath)
        {
            // Assert
            Assert.Throws<ArgumentNullException>(
                () => XMLSerializer.Deserialize<List<FlightLogInfo>>(filePath));
        }

        [Fact]
        public void Serialize_ValidFilePathWithMismatchingDataType_ThrowsInvalidOperationException()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(
                () => XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePathsForTesting.PathForXmlFileUsedForInsert));
        }
    }
}
