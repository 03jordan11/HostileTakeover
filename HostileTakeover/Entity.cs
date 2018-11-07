using System;
using System.Drawing;

namespace HostileTakeover {

    class Entity : GameObject {

        public Sprite Sprite { get; private set; }
        public Position Pos { get; set; }

        public void Tick() {
            Sprite.NextFrame();
        }

        public void Render(Graphics g) {
            g.DrawImage(Sprite.CurrentImage(), (int) Pos.X, (int) Pos.Y);
        }

    }

}
