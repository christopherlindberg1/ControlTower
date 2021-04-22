using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{
    interface ITextFileFlightLogger
    {
        List<FlightLogInfo> GetLog(string filePath);

        void SaveEntryInLog(string filePath, FlightLogInfo objectToSave);
    }
}
