using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace HostileTakeover {

    [Serializable()]
    public class Entity : GameObject, ISerializable {

        public Sprite Sprite { get; set; }
        public Position Pos { get; set; }

        public Entity() {
            Pos = new Position();
        }

        public void Tick() {
            Sprite.NextFrame();
        }

        public void Render(Graphics g) {
            g.DrawImageUnscaled(Sprite.CurrentImage(), (int) Pos.X, (int) Pos.Y);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Pos", Pos);
        }
    }

}
