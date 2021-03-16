using AppFeatures.Models;
using DataAccess;
using DataAccess.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Utility;

namespace AppFeatures
{
    public class FlightLogger
    {
        public void OnTakeOff(object source, TakeOffEventArgs e)
        {
            TextFileUtility.AppendLineToTextFile(
                FilePaths.SampleFlightLogFilePath, FormatTakeOffLogMessage(e));
        }

        public void OnLanded(object source, LandEventArgs e)
        {
            TextFileUtility.AppendLineToTextFile(
                FilePaths.SampleFlightLogFilePath, FormatLandingLogMessage(e));
        }

        private string FormatTakeOffLogMessage(TakeOffEventArgs e)
        {
            string flightCodeFormatted = e.FlightCode.PadRight(13);
            string takeOffLabelFormatted = "took off".PadRight(11);
            
            return $"Flight: { flightCodeFormatted } { takeOffLabelFormatted } { e.DateTime.ToString() }";
        }

        private string FormatLandingLogMessage(LandEventArgs e)
        {
            string flightCodeFormatted = e.FlightCode.PadRight(13);
            string landedLabelFormatted = "landed".PadRight(11);

            return $"Flight: { flightCodeFormatted } { landedLabelFormatted } { e.DateTime.ToString() }";
        }
    }
}
