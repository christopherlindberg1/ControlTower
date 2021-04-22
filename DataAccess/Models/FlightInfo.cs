using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    /// <summary>
    /// Model used to store flight data that will be used in the ListView in the main window.
    /// </summary>
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
