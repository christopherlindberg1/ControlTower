using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class XmlFlightLogger : ITextFileFlightLogger
    {
        public List<FlightLogInfo> GetLog(string filePath)
        {
            throw new NotImplementedException();
        }

        public void SaveEntryInLog(string filePath, FlightLogInfo objectToSave)
        {
            throw new NotImplementedException();
        }
    }
}
