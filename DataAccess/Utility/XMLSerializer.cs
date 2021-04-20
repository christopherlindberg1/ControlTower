using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccess.Utility
{
    /// <summary>
    /// Class providing static methods for XML serialization.
    /// </summary>
    public class XMLSerializer
    {
        /// <summary>
        /// Serializes an object to a given file path.
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize</typeparam>
        /// <param name="filePath">File path for where the serialized data should be stored</param>
        /// <param name="obj">Object to serialize</param>
        public static void Serialize<T>(string filePath, T obj)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath", "filePath cannot be null.");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                try
                {
                    serializer.Serialize(streamWriter, obj);
                }
                // Should catch and handle more specific exceptions
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Deserializes stored data from an XML-file into an object of the corresponding type.
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize</typeparam>
        /// <param name="filePath">File path for where the serialized data should be stored</param>
        /// <returns>Returns an object containing data that was deserialized from a file.</returns>
        public static T Deserialize<T>(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath", "filePath cannot be null.");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                try
                {
                    return (T)serializer.Deserialize(streamReader);
                }
                // Should catch and handle more specific exceptions
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
