using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
namespace HostileTakeover
{
    static class HostileTakeover {

        public static Bitmap Canvas;

        public static List<GameObject> GameObjects;

        public static Size Resolution;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // Set resolution and create canvas
            Resolution = new Size(800, 600);
            Canvas = new Bitmap(Resolution.Width, Resolution.Height);
            Graphics g = Graphics.FromImage(Canvas);


            SolidBrush brush = new SolidBrush(Color.Red);
            g.FillRectangle(brush, 700, 500, 100, 100);

            // Create the window
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Window(Resolution));
        }

    }
}
