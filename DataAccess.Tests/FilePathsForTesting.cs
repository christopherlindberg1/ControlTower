using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Class containing file paths that used when testing the app.
    /// </summary>
    public static class FilePathsForTesting
    {
        public static string DataStorageRootFolderPathForTesting
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\DataAccess.Tests\Storage\"));
            }
        }

        public static string PathForXmlFileWithData
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(DataStorageRootFolderPathForTesting,
                        @".\FlightLogs\FlightLog.xml"));
            }
        }

        public static string PathForXmlFileUsedForInsert
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(DataStorageRootFolderPathForTesting,
                        @".\FlightLogs\FlightLogForInsert.xml"));
            }
        }
    }
}
