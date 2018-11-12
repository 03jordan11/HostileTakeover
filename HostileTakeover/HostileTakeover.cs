using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HostileTakeover {

    static class HostileTakeover {
         
        public static Bitmap Canvas;

        public static List<GameObject> GameObjects;

        public static Size Resolution;

        private static System.Windows.Threading.DispatcherTimer Timer;

        private static Window Window;

        public static Keyboard Keyboard;

        private static Graphics g;

        public static Player player;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {


            Position z = new Position { X = 1, Y = 1, Z = 1 };
            BinaryFormatter bf = new BinaryFormatter();
            System.IO.Directory.CreateDirectory("save/levels/");
            FileStream fs = File.Open("save/levels/pos", FileMode.Create);
            bf.Serialize(fs, z);
            fs.Close();
            fs = File.Open("save/levels/pos", FileMode.Open);
            z = null;
            z = (Position)bf.Deserialize(fs);
            Console.WriteLine(z.X + z.Y + z.Z);
            fs.Close();


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
            Window = new Window(Resolution);

            Image grassImage = Image.FromFile("assets/sprites/characterModels/64x64_grass.png");
            
           
            for(int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    GameObjects.Add(new Entity
                    {
                        Pos = new Position
                        {
                            X = i*64,
                            Y = j*64
                        },
                        Sprite = new Sprite(grassImage)
                    });
                }

            }

            // load in sprites images and add a sprite
            Image i1 = Image.FromFile("assets/sprites/characterModels/32_x_32_platform_character_idle_0.png");
            Image i2 = Image.FromFile("assets/sprites/characterModels/32_x_32_platform_character_idle_1.png");
            Image i3 = Image.FromFile("assets/sprites/characterModels/32_x_32_platform_character_idle_2.png");
            Image i4 = Image.FromFile("assets/sprites/characterModels/32_x_32_platform_character_idle_3.png");
            List<Image> images = new List<Image>() { i1, i2, i3, i4};
            var entity = new Entity
            {
                Sprite = new Sprite(images)
            };
            GameObjects.Add(entity);

            Image test = Image.FromFile("assets/sprites/characterModels/player.png");
            player = (new Player
            {
                Pos = new Position
                {
                    X = 100,
                    Y = 100
                },
                Sprite = new Sprite(test)
            });
            GameObjects.Add(player);

            // Begin Execution
            Timer.Start();
            Application.Run(Window);

        }

        static void Tick(Object Sender, EventArgs e) {
            if (Keyboard.isDown(Keys.W))
                player.Pos.Y -= 1;
            if (Keyboard.isDown(Keys.A))
                player.Pos.X -= 1;
            if (Keyboard.isDown(Keys.S))
                player.Pos.Y += 1;
            if (Keyboard.isDown(Keys.D))
                player.Pos.X += 1;
            g.Clear(Color.Empty); // Empty the frame
            foreach (GameObject go in GameObjects) {
                go.Tick();
                go.Render(g);
            }
            Window.Refresh();
        }
        public static void AddGameObject(bool isPlayer, Sprite sprite, float x = 0, float y = 0, float z = 0)
        {
            var model = isPlayer ? new Player() : new Entity();
            model.Sprite = sprite;
            model.Pos = new Position { X = x, Y = y, Z = z };
            GameObjects.Add(model);
        }
        public static void Resize(Size size) {
            HostileTakeover.g.Dispose();
            Canvas.Dispose();
            HostileTakeover.Resolution = size;
            Canvas = new Bitmap(Resolution.Width, Resolution.Height);
            g = Graphics.FromImage(Canvas);
        }

        public static void Stop() {
            Console.WriteLine("cleanup");
            HostileTakeover.Timer.IsEnabled = false;
            HostileTakeover.Timer.Stop();
            HostileTakeover.g.Dispose();
            Canvas.Dispose();
        }

    }

}
