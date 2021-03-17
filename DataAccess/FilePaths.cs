﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Class containing file paths that are used within the app.
    /// </summary>
    public class FilePaths
    {
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

        public static string SampleFlightLogFilePath
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(FlightLogsDataFolderPath, @".\SampleLog.xml"));
            }
        }
    }
}