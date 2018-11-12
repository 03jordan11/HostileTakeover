using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace HostileTakeover {

    public class HostileTakeover {

        public static HostileTakeover Instance { get ; set; }

        public Bitmap Canvas { get; }

        public List<GameObject> GameObjects { get; }

        public Size Resolution { get { return Window.Size; } }

        private Window Window;

        public Keyboard Keyboard { get; }

        private Graphics g { get { return Window.g; } }

        public Player player;

        public Stopwatch StopWatch { get; private set; }

        private Thread GraphicsThread;

        public Boolean Running { get; private set; }

        private int fps;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Instance = new HostileTakeover();

            // testing serializable data
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

            Image grassImage = Image.FromFile("assets/sprites/characterModels/64x64_grass.png");


            for (int i = 0; i < 100; i++) {
                for (int j = 0; j < 100; j++) {
                    Instance.GameObjects.Add(new Entity {
                        Pos = new Position {
                            X = i * 64,
                            Y = j * 64
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
            List<Image> images = new List<Image>() { i1, i2, i3, i4 };
            var entity = new Entity {
                Sprite = new Sprite(images, new List<int>() { 1, 2, 3, 4 })
            };
            Instance.GameObjects.Add(entity);

            Image test = Image.FromFile("assets/sprites/characterModels/david.png");
            Instance.player = (new Player {
                Pos = new Position {
                    X = 100,
                    Y = 100
                },
                Sprite = new Sprite(test, 0, 0, 64, 64, new List<int>() { 150, 150, 150, 150, 150 })
            });
            Instance.GameObjects.Add(Instance.player);

            Instance.Start();
        }

        public HostileTakeover() {
            // Init Stuff
            Keyboard = new Keyboard();
            GameObjects = new List<GameObject>();
            StopWatch = new Stopwatch();

            // Create the window
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Window = new Window(new Size(800, 600), Keyboard);

        }

        private void RunGui() {
            Application.Run(Window);
        }

        public void Start() {
            StopWatch.Start();
            GraphicsThread = new Thread(RunGui);
            GraphicsThread.Start();
            Running = true;
            long lastSecond = 0;
            int frames = 0;
            while (Running && Window.Running) {
                Tick();
                Render();
                ++frames;
                if (StopWatch.ElapsedMilliseconds - lastSecond >= 1000) {
                    lastSecond = StopWatch.ElapsedMilliseconds;
                    fps = frames;
                    frames = 0;
                }
            }
        }

        public void Stop() {
            Window.Close();
            Running = false;
        }

        private void Tick() {
            if (Keyboard.isDown(Keys.W))
                player.Pos.Y -= 1;
            if (Keyboard.isDown(Keys.A))
                player.Pos.X -= 1;
            if (Keyboard.isDown(Keys.S))
                player.Pos.Y += 1;
            if (Keyboard.isDown(Keys.D))
                player.Pos.X += 1;
            foreach (GameObject go in GameObjects) {
                go.Tick();
            }
        }

        private void Render() {
            g.Clear(Color.Empty); // Empty the frame
            foreach (GameObject go in GameObjects) {
                go.Render(g);
            }
            Brush b = new SolidBrush(Color.White);
            g.DrawString("" + fps, SystemFonts.DefaultFont, b, 0, 0);
            // Remove this when fixed
            try {
                Window.Invoke(new Action(() => { Window.Refresh(); }));
            } catch (Exception e) {
            }
        }

        public void AddGameObject(bool isPlayer, Sprite sprite, float x = 0, float y = 0, float z = 0)
        {
            var model = isPlayer ? new Player() : new Entity();
            model.Sprite = sprite;
            model.Pos = new Position { X = x, Y = y, Z = z };
            GameObjects.Add(model);
        }

    }

}
