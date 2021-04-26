using System;

namespace AppFeatures
{
    public interface IAirlineImageGenerator
    {
        Uri GetImageUri(string flightCode);
    }
}