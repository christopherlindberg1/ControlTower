using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures.Models
{
    public class FlightLogInfo
    {
        public string FlightCode { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }
    }
}
