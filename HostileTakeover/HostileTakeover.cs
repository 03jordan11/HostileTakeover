using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace HostileTakeover {

    static class HostileTakeover {

        public static Bitmap Canvas;

        public static List<GameObject> GameObjects;

        public static Size Resolution;

        private static System.Windows.Threading.DispatcherTimer Timer;

        private static Window Window;

        public static Keyboard Keyboard;

        private static Graphics g;

        public static Entity entity;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // Init Stuff
            Keyboard = new Keyboard();
            GameObjects = new List<GameObject>();

            // Set resolution and create canvas
            Resolution = new Size(800, 600);
            Canvas = new Bitmap(Resolution.Width, Resolution.Height);
            Canvas.MakeTransparent();
            g = Graphics.FromImage(Canvas);

            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60); // days, hours, minutes, seconds, milliseconds
            Timer.Tick += new EventHandler(Tick);

            // Create the window
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HostileTakeover.Window = new Window(Resolution);
            
            // load in sprites images and add a sprite
            Image i1 = Image.FromFile("32 x 32 platform character_idle_0.png");
            Image i2 = Image.FromFile("32 x 32 platform character_idle_1.png");
            Image i3 = Image.FromFile("32 x 32 platform character_idle_2.png");
            Image i4 = Image.FromFile("32 x 32 platform character_idle_3.png");
            List<Image> images = new List<Image>() { i1, i2, i3, i4};
            entity = new Entity();
            entity.Sprite = new Sprite(images);
            GameObjects.Add(entity);

            // Begin Execution
            Timer.Start();
            Application.Run(Window);

        }

        static void Tick(Object Sender, EventArgs e) {
            if (Keyboard.isDown(Keys.W))
                entity.Pos.Y -= 1;
            if (Keyboard.isDown(Keys.A))
                entity.Pos.X -= 1;
            if (Keyboard.isDown(Keys.S))
                entity.Pos.Y += 1;
            if (Keyboard.isDown(Keys.D))
                entity.Pos.X += 1;
            foreach (GameObject go in GameObjects) {
                go.Tick();
                g.Clear(Color.Empty); // Empty the frame
                go.Render(g);
            }
            Window.Refresh();
        }

    }

}
