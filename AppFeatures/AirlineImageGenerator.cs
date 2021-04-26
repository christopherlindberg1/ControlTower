using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures
{
    public class AirlineImageGenerator : IAirlineImageGenerator
    {
        public Uri GetImageUri(string flightCode)
        {
            Uri pathToImage = new Uri("/Images/Airline-unknown.jpg", UriKind.Relative);

            if (flightCode.ToLower().Contains("dlh"))
            {
                pathToImage = new Uri("/Images/Airline-dlh.jpg", UriKind.Relative);
            }
            else if (flightCode.ToLower().Contains("rys"))
            {
                pathToImage = new Uri("/Images/Airline-rys.jpg", UriKind.Relative);
            }
            else if (flightCode.ToLower().Contains("sas"))
            {
                pathToImage = new Uri("/Images/Airline-sas.jpg", UriKind.Relative);
            }

            return pathToImage;
        }
    }
}
