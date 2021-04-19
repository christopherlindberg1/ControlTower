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
        public void Deserialize_GetEntireFlightLog_ShouldWork()
        {
            // Arrange


            // Act
            List<FlightLogInfo> flightLog = 
                XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePaths.FilePathForXmlTestFile);


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
    }
}
