using DataAccess.Models;
using DataAccess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class XmlFlightLogger : ITextFileFlightLogger
    {
        private readonly string _filePath;
        private readonly List<FlightLogInfo> _flightLog;
        
        public XmlFlightLogger(string filePath)
        {
            _filePath = filePath;
        }

        public string FilePath { get; }

        public List<FlightLogInfo> FlightLog
        {
            get => _flightLog ?? GetLog();
        }

        public List<FlightLogInfo> GetLog()
        {
            return XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePath);
        }

        public void SaveEntryInLog(FlightLogInfo flightLogEntry)
        {
            FlightLog.Add(flightLogEntry);

            XMLSerializer.Serialize<List<FlightLogInfo>>(FilePath, FlightLog);
        }
    }
}
