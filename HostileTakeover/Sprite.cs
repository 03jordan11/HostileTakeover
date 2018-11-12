using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace HostileTakeover {

    [Serializable()]
    public class Sprite {

        public Boolean Animated { get; private set; }
        public int Frames { get; private set; }
        public int CurrentFrame { get; private set; }
        public List<Image> Images { get; private set; }
        public List<int> FrameTimes { get; private set; }
        private bool SpriteSheet;
        private long LastFrame;

        public Sprite(List<Image> images, List<int> frameTimes) {
            this.Images = images;
            this.FrameTimes = frameTimes;
            this.Animated = true;
            this.Frames = Images.Count;
            this.CurrentFrame = 0;
            LastFrame = 0;
        }


        public Sprite(Image image, int offsetX, int offsetY, int unitWidth, int unitHeight, List<int> frameTimes) {
            this.FrameTimes = frameTimes;
            this.Images = new List<Image>();
            this.Animated = true;
            this.Frames = 0;
            this.CurrentFrame = 0;
            this.SpriteSheet = true;
            Bitmap imageBitmap = (Bitmap) image;
            while(offsetY < image.Height) {
                Bitmap keyFrame = imageBitmap.Clone(new Rectangle(offsetX, offsetY, unitWidth, unitHeight), image.PixelFormat);
                this.Images.Add(keyFrame);
                ++this.Frames;
                offsetY += unitHeight;
            }
            LastFrame = 0;
        }

        public Sprite(Image Image) {
            this.Images = new List<Image>();
            this.Images.Add(Image);
            this.FrameTimes = new List<int>();
            this.Animated = false;
            this.Frames = 1;
            this.CurrentFrame = 0;
        }

        public void NextFrame() {
            if (!Animated) return;
                if (HostileTakeover.Instance.StopWatch.ElapsedMilliseconds - LastFrame >= FrameTimes[CurrentFrame]) {
                LastFrame = HostileTakeover.Instance.StopWatch.ElapsedMilliseconds;
                if (CurrentFrame + 1 < Frames)
                    ++CurrentFrame;
                else
                    CurrentFrame = 0;
            }
        }

        public Image CurrentImage() {
            return Images[CurrentFrame];
        }

        public void Reset() {
            this.CurrentFrame = -1;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            
        }
    }
}
