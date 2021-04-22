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



        // ===================== Properties ===================== //

        public string FilePath { get => _filePath; }



        // ===================== Methods ===================== //

        public XmlFlightLogger(string filePath)
        {
            _filePath = filePath;
        }
        
        public List<FlightLogInfo> GetLog()
        {
            return XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePath);
        }

        public void SaveEntryInLog(FlightLogInfo flightLogEntry)
        {
            List<FlightLogInfo> flightLog = GetLog();

            flightLog.Add(flightLogEntry);

            XMLSerializer.Serialize<List<FlightLogInfo>>(FilePath, flightLog);
        }
    }
}
