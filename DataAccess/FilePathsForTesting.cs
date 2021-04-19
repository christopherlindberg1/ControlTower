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
    public static partial class FilePaths
    {
        public static string DataStorageRootFolderPathForTesting
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\DataAccess.Tests\Storage\"));
            }
        }

        public static string FilePathForXmlTestFileWithData
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(DataStorageRootFolderPathForTesting,
                        @".\FlightLogs\FlightLog.xml"));
            }
        }

        public static string FilePathForXmlTestFileWithoutData
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
