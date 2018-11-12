using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostileTakeover {
    
    public partial class Window : Form {

        public Keyboard Keyboard { get; }
        public Bitmap Canvas { get; private set; }
        public Graphics g { get; private set; }
        public bool Running { get; private set; }

        public Window(Size resolution, Keyboard keyboard) {
            // Disable layouts
            SuspendLayout();

            Running = true;
            this.Keyboard = keyboard;
            this.Size = resolution;
            CreateCanvas();

            // Enable double buffering
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Reduce Flickering
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            // Set the window size
            this.Size = resolution;

            // Center Window
            this.StartPosition = FormStartPosition.CenterScreen;

            // Allows component to get key events before being passed to focused component
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyRelease);

            // Set window title
            this.Text = "Hostile Takeover";

            // Set the component name
            this.Name = "Window";

            // Not working
            this.Focus();

            // Commenting this function out disables the designer
            // InitializeComponent();
        }

        /// <summary>
        /// Paints to the screen
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.DrawImageUnscaled(Canvas, 0, 0);

            Brush b = new SolidBrush(Color.Red);
            //e.Graphics.FillRectangle(b, 0, 0, 100, 100);
        }


        protected override void OnResize(EventArgs e) {
            CreateCanvas();
        }

        private void CreateCanvas() {
            if(g != null)
                g.Dispose();
            if (Canvas != null)
                Canvas.Dispose();
            Canvas = new Bitmap(Size.Width, Size.Height);
            g = Graphics.FromImage(Canvas);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Window
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Window";
            this.Load += new System.EventHandler(this.Window_Load);
            this.ResumeLayout(false);

        }

        private void Window_Load(object sender, EventArgs e) {}

        private void KeyPress(Object sender, KeyEventArgs e) {
            Keyboard.setKey(e.KeyCode, true);
        }

        private void KeyRelease(Object sender, KeyEventArgs e) {
            Keyboard.setKey(e.KeyCode, false);
        }

        protected override void OnClosed(EventArgs e) {
            Running = false;
        }

    }

}
