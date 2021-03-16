using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utility
{
    public class TextFileUtility
    {
        public static void AppendLineToTextFile(string filePath, string content)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(
                FilePaths.SampleFlightLogFilePath, append: true))
                {
                    streamWriter.WriteLineAsync(content);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<string> GetAllLines(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(FilePaths.SampleFlightLogFilePath))
            {
                string data = streamReader.ReadToEnd();

                return data.Split('\n').ToList<string>();
            }
        }
    }
}
