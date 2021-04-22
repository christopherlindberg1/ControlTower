using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    /// <summary>
    /// Model used to store flight data that will be stored in a log file.
    /// </summary>
    public class FlightLogInfo
    {
        public string FlightCode { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }
    }
}
