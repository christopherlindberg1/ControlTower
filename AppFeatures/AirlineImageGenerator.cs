using DataAccess;
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
            Uri pathToImage = new Uri($"{ FilePaths.ImagesRootFilder }Airline-unknown.jpg", UriKind.Absolute);

            if (flightCode.ToLower().Contains("dlh"))
            {
                pathToImage = new Uri($"{ FilePaths.ImagesRootFilder }Airline-dlh.jpg", UriKind.Absolute);
            }
            else if (flightCode.ToLower().Contains("rys"))
            {
                pathToImage = new Uri($"{ FilePaths.ImagesRootFilder }Airline-rys.jpg", UriKind.Absolute);
            }
            else if (flightCode.ToLower().Contains("sas"))
            {
                pathToImage = new Uri($"{ FilePaths.ImagesRootFilder }Airline-sas.jpg", UriKind.Absolute);
            }

            return pathToImage;
        }
    }
}
