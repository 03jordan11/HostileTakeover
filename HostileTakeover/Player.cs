using System;
using System.Runtime.Serialization;

namespace HostileTakeover {

    [Serializable()]
    public class Player : Entity, ISerializable
    {
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

}
