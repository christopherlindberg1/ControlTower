using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures
{
    public class LandEventArgs : EventArgs 
    {
        public string _flightCode;


        /// <summary>
        /// Flight code of the airplane
        /// </summary>
        public string FlightCode
        {
            get => _flightCode;

            set => _flightCode = value ??
                throw new ArgumentNullException("FlightCode", "FlightCode cannot be null");
        }

        /// <summary>
        /// Readonly property for status, since the status is always the same when
        /// the landing event occurs.
        /// </summary>
        public string Status { get; } = "Landed";
    }
}
