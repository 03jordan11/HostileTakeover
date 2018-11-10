using System;
using System.Drawing;

namespace HostileTakeover {

    public class Entity : GameObject {

        public static int FrameTime;
        public Sprite Sprite { get; set; }
        public Position Pos { get; set; }

        public Entity() {
            Pos = new Position();
        }

        public void Tick() {
            Sprite.NextFrame();
        }

        public void Render(Graphics g) {
            //debug
            System.Console.WriteLine(Sprite.CurrentFrame);
            g.DrawImageUnscaled(Sprite.CurrentImage(), (int) Pos.X, (int) Pos.Y);
        }

    }

}
