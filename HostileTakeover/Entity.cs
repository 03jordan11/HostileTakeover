using System;
using System.Drawing;

namespace HostileTakeover {

    class Entity : GameObject {

        public Sprite Sprite { get; set; }
        public Position Pos { get; set; }

        public Entity() {
            Pos = new Position();
        }

        public void Tick() {
            Sprite.NextFrame();
        }

        public void Render(Graphics g) {
            System.Console.WriteLine(Sprite.CurrentFrame);
            g.DrawImageUnscaled(Sprite.CurrentImage(), (int) Pos.X, (int) Pos.Y);
        }

    }

}
