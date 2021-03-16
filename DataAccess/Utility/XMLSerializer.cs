using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccess.Serialization
{
    public class XMLSerializer
    {
        public static void Serialize<T>(string filePath, T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                try
                {
                    serializer.Serialize(streamWriter, obj);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public static T Deserialize<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                try
                {
                    return (T)serializer.Deserialize(streamReader);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
