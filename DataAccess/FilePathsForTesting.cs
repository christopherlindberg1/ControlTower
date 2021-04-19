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
                    Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\DataAccess\Storage\"));
            }
        }

        public static string FilePathForXmlTestFile
        {
            get
            {
                return Path.GetFullPath(
                    Path.Combine(DataStorageRootFolderPathForTesting,
                        @".\FlightLogsTestFiles\FlightLogSample.xml"));
            }
        }
    }
}
