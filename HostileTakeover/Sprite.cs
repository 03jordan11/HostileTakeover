using System;
using System.Collections.Generic;
using System.Drawing;

namespace HostileTakeover
{
    public class Sprite {

        public Boolean Animated { get; private set; }
        public int Frames { get; private set; }
        public int CurrentFrame { get; private set; }
        public List<Image> Images { get; private set; }

        public Sprite(List<Image> Images) {
            this.Images = Images;
            this.Animated = true;
            this.Frames = Images.Count;
            this.CurrentFrame = -1;
        }

        public Sprite(Image Image) {
            this.Images = new List<Image>();
            this.Images.Add(Image);
            this.Animated = false;
            this.Frames = 1;
            this.CurrentFrame = 0;
        }

        public void NextFrame() {
            if (!Animated) return;
            
            if (CurrentFrame + 1 < Frames)
                ++CurrentFrame;
            else
                CurrentFrame = 0;
        }

        public Image CurrentImage() {
            return Images[CurrentFrame];
        }

        public void Reset() {
            this.CurrentFrame = -1;
        }

    }
}
