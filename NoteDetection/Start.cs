/* Author: Bethany Weddle
 * Class: Start.cs
 * */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NoteDetection
{
    /// <summary>
    /// Creates the Piano and Sheet Music with the specified BPM and Chromatic 
    /// </summary>
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        /// <summary>
        /// User starts up the Piano and Sheet Music Forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxOpen_Click(object sender, EventArgs e)
        {
            
            Chromatic type = Chromatic.Natural;

            if(uxSharp.Checked == true)
                type = Chromatic.Sharp;
            if (uxFlats.Checked)
                type = Chromatic.Flat;

            // Positions the Forms so they aren't on top of each other
            SheetMusic sheet = new SheetMusic();
            sheet.Location = new Point(500, 200);
            Piano piano = new Piano(Convert.ToInt32(BPM.Text), type, sheet);
            piano.Location = new Point(250, 0);

            // Shows the Piano and hides the Main Form
            piano.Show();
            this.Hide();
            
        }
    }
}
