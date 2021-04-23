using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures.FlightActionsEventArgs
{
    /// <summary>
    /// Manages data that is of interest for when an airplane changes route.
    /// </summary>
    public class ChangeRouteEventArgs : EventArgs
    {
        private string _flightCode;
        private string _route;

        

        public string FlightCode
        {
            get => _flightCode;

            set => _flightCode = value ??
                throw new ArgumentNullException("FlightCode", "FlightCode cannot be null");
        }

        public string Route
        {
            get => _route;

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Route cannot be null", "Route");
                }

                _route = value;
            }
        }
    }
}
