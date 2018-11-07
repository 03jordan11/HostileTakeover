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

        public Window(Size resolution) {

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

            // Set window title
            this.Text = "Hostile Takeover";

            // Set the component name
            this.Name = "Window";

            // Commenting this function out disables the designer
            // InitializeComponent();
        }

        /// <summary>
        /// Paints to the screen
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.DrawImageUnscaled(HostileTakeover.Canvas, 0, 0);
        }


        protected override void OnResize(EventArgs e) {
            HostileTakeover.Resolution = this.Size;
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

        private void Window_Load(object sender, EventArgs e)
        {

        }
    }

}
