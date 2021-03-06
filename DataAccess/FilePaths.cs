using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Class containing file paths that used within the app.
    /// </summary>
    public static class FilePaths
    {
        public static string SolutionRootFolder
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
            }
        }

        public static string ImagesRootFilder
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(SolutionRootFolder, @".\ControlTowerWPF\Images\"));
            }
        }

        public static string DataStorageRootFolderPath
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\DataAccess\Storage\"));
            }
        }

        public static string FlightLogsDataFolderPath
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(DataStorageRootFolderPath, @".\FlightLogs\"));
            }
        }

        public static string FlightLogFilePath
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(FlightLogsDataFolderPath, @".\FlightLog.xml"));
            }
        }
    }
}