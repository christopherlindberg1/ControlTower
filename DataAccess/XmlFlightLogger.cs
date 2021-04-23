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
        
        /// <summary>
        /// Gets the entire flight log stored in an XML-file
        /// </summary>
        /// <returns>List with FlightLigInfo objects</returns>
        public List<FlightLogInfo> GetLog()
        {
            return XMLSerializer.Deserialize<List<FlightLogInfo>>(FilePath);
        }

        /// <summary>
        /// Adds a FlightLogInfo object to the collection stored in an XML-file.
        /// </summary>
        /// <param name="flightLogEntry">Object containing information about an action</param>
        public void SaveEntryInLog(FlightLogInfo flightLogEntry)
        {
            List<FlightLogInfo> flightLog = GetLog();

            flightLog.Add(flightLogEntry);

            XMLSerializer.Serialize<List<FlightLogInfo>>(FilePath, flightLog);
        }
    }
}
