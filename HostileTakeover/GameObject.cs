using System;
using System.Drawing;

namespace HostileTakeover {

    /// <summary>
    /// A GameObject is simply something that can be updated or rendered from within the game loop.
    /// </summary>
    public interface GameObject {

        void Render(Graphics g);
        void Tick();

    }
}
