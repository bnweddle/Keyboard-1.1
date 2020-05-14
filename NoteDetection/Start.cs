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
            bool rests = true;
            Chromatic type = Chromatic.Natural;

            if(Off.Checked == false && On.Checked == false)
            {
                MessageBox.Show("Must Select to Show Rests or Not before continuing");
            }
            else if(Off.Checked == true && On.Checked == true)
            {
                MessageBox.Show("Must Select only On or Off, not both");
            }
            else if(Off.Checked == true || On.Checked == true)
            {
                if (uxSharp.Checked == true)
                    type = Chromatic.Sharp;
                if (uxFlats.Checked)
                    type = Chromatic.Flat;

                if (On.Checked == true)
                {
                    rests = true;
                    Off.Checked = false;
                    Off.CheckState = CheckState.Unchecked;
                    Off.Update();
                    this.Update();
                }
                if (Off.Checked == true)
                {
                    rests = false;
                    On.Checked = false;
                    On.CheckState = CheckState.Unchecked;
                    On.Update();
                    this.Update();
                }

                // Positions the Forms so they aren't on top of each other
                SheetMusic sheet = new SheetMusic();
                sheet.Location = new Point(500, 200);
                Piano piano = new Piano(Convert.ToInt32(BPM.Text), type, sheet, rests);
                piano.Location = new Point(250, 0);

                // Shows the Piano and hides the Main Form
                piano.Show();
                this.Hide();

            }

            
        }
    }
}
