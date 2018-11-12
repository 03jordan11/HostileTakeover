using System;
using System.Runtime.Serialization;

namespace HostileTakeover
{
    [Serializable()]
    public class Position : ISerializable {

        #region contsructors
        public Position(SerializationInfo info, StreamingContext context) {
            X = info.GetSingle("X");
            Y = info.GetSingle("Y");
            Z = info.GetSingle("Z");
        }
        #endregion

        public Position() { }

        #region Attributes
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        #endregion

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("X", X);
            info.AddValue("Y", X);
            info.AddValue("Z", X);
        }



    }

}
