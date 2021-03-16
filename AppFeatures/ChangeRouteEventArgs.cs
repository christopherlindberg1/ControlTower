using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures
{
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


                //int heading;

                //if (int.TryParse(value, out heading) == false)
                //{
                //    throw new ArgumentException("Route must be digits only", "Route");
                //}

                //if (heading < 0 || heading > 350)
                //{
                //    throw new ArgumentOutOfRangeException("Route", "Route must be between 0-350");
                //}

                //if (heading % 10 != 0)
                //{
                //    throw new ArgumentException(
                //        "Route must be a multiple of 10 in the range 0-350 (e.g 0, 70, 240)",
                //        "Route");
                //}

            }
        }
    }
}
