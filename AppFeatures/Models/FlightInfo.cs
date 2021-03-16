using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures.Models
{
    public class FlightInfo
    {
        public string FlightCode { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public string DisplayDateTime
        {
            get => $"{ DateTime.ToLongTimeString() } - { DateTime.ToShortDateString() }";
        }
    }
}
