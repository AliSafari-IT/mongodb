using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace InWinFormsApp.Forms
{
    public class WindowOverlay 
    {

        /// <summary>
        /// Physical overlay window.
        /// </summary>
        public Form Window { get; private set; }
        public WindowOverlay()
        {

            // create window
            Window = new Form
            {
                Name = "Overlay Window",
                Text = "Overlay Window",
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.None,
                TopMost = true,
                Width = 16,
                Height = 16,
                Left = -32000,
                Top = -32000,
                StartPosition = FormStartPosition.Manual,
            };

            Window.Load += (sender, args) =>
            {

            };

            // show window
            Window.Show();
        }

    }
}
