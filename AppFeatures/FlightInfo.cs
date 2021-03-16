using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures
{
    public class FlightInfo
    {
        public string FlightCode { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }

        public string DisplayDateTime
        {
            get => $"{ DateTime.ToLongTimeString() } - { DateTime.ToShortDateString() }";
        }
    }
}
