using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFeatures;
using DataAccess;
using Xunit;

namespace AppFeatures.Tests
{
    public class FlightLoggerTests
    {
        private static FlightLogger _flightLogger = new FlightLogger(FilePaths.FilePathForXmlTestFile);
    }
}
