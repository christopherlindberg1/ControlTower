using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFeatures.Models
{
    [Serializable]
    public class FlightLogInfo : ISerializable
    {
        public string FlightCode { get; set; }
        public DateTime DateTimeTakeOff { get; set; }
        public DateTime DateTimeLanding { get; set; }

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("FlightCode", FlightCode);
        //    info.AddValue("DateTimeTakeOff", DateTimeTakeOff);
        //    info.AddValue("DateTimeLanding", DateTimeLanding);
        //}

        //public FlightLogInfo()
        //{

        //}

        //public FlightLogInfo(SerializationInfo info, StreamingContext context)
        //{
        //    FlightCode = (string)info.GetValue("FlightCode", typeof(string));
        //    DateTimeTakeOff = (DateTime)info.GetValue("DateTimeTakeOff", typeof(DateTime));
        //    DateTimeLanding = (DateTime)info.GetValue("DateTimeLanding", typeof(DateTime));
        //}
    }
}
