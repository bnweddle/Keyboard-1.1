/* Author: Bethany Weddle
 * Class: Piano.cs
 * Used PianoControl from MidiKit on Github with Free Software License
 * */
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

namespace NoteDetection
{
    /// <summary>
    /// Piano class for displaying the Keyboard
    /// </summary>
    public partial class Piano : Form
    {
        private int outDeviceID = 0;

        private OutputDevice outDevice;

        // Old and New timers for Note duration
        Stopwatch[] oldTimers = new Stopwatch[127]; 
        Stopwatch[] currentTimers = new Stopwatch[127];

        DateTime startTime; 

        // Note objects
        NoteEstimator noteEstimator;
        Note note = new Note();
        Keys keys = new Keys();
        SheetMusic sheetForm;

        // private variables passed between Forms
        private int offset = 75;
        private bool thirds = false;
        private bool chrom = false;
        private Chromatic chromatic = Chromatic.Natural;

        // index/number of Key pressed
        int whitePressed;
        int blackPressed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bpm">Beats Per Minute</param>
        /// <param name="type">Chromatic Type</param>
        /// <param name="form">Sheet Music</param>
        public Piano(int bpm, Chromatic type, SheetMusic form)
        {
            InitializeComponent();
            sheetForm = form;
            chromatic = type; 
            noteEstimator = new NoteEstimator(bpm);
            sheetForm.Show();

            // Initializes the Stopwatch Timers
            for (int i = 0; i < oldTimers.Length; i++)
            {
                oldTimers[i] = new Stopwatch();
                currentTimers[i] = new Stopwatch();
            }
            this.pianoControl.Size = this.Size;
        }

        // Loads the Device for the Piano Control
        protected override void OnLoad(EventArgs e)
        {
            if (OutputDevice.DeviceCount == 0)
            {
                Close();
            }
            else
            {
                try
                {
                    outDevice = new OutputDevice(outDeviceID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!");
                    Close();
                }
            }

            base.OnLoad(e);
        }

        int noteID;
        // For when the Keyboard Note or Mouse  Note is pressed
        private void PianoControl_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            oldTimers[e.NoteID].Start();
            noteID = e.NoteID;
            startTime = DateTime.UtcNow;
            System.Diagnostics.Debug.WriteLine($"{startTime} time button pushed");
            System.Diagnostics.Debug.WriteLine($"{e.NoteID} noteID");
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));

            offset += 45;
        }

        // For when the Keyboard Note or Mouse Note is released
        private void PianoControl_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {   
            oldTimers[e.NoteID].Stop();       
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));

            currentTimers[e.NoteID] = oldTimers[e.NoteID];
            long duration = currentTimers[e.NoteID].ElapsedMilliseconds.Round(100);

            // Checking for Thirds
            Timing symbols = noteEstimator.GetNoteFromDuration(duration);
            if (symbols == Timing.ThirdHalf || symbols == Timing.ThirdQuart 
                || symbols == Timing.ThirdEigth || symbols == Timing.ThirdSixteen)
                thirds = true;
            else
                thirds = false;

            System.Diagnostics.Debug.WriteLine($"{symbols } timing");

            // Setting the Positions
            whitePressed = keys.WhiteKeyPress(e.NoteID, out chrom);
            blackPressed = keys.BlackKeyPress(e.NoteID, out chrom);
            keys.SetPositions(blackPressed, whitePressed, chromatic, chrom);

            System.Diagnostics.Debug.WriteLine($"{whitePressed } white note");
            System.Diagnostics.Debug.WriteLine($"{blackPressed } black note");

            sheetForm.SetChromatic(chrom, chromatic);

            // Globally shared variables
            Global.Symbol = note.GetNoteSymbol(symbols);
            Global.Chromatic = note.GetChromaticSymbol(chromatic);
            Timing time;
            Global.Image = note.GetImage(symbols, out time);
            Global.Time = time;

            sheetForm.UpdatePaint(offset, thirds, keys.GetPosition(e.NoteID));

            oldTimers[e.NoteID].Reset();
        }

        // When the user hits a Key by Mouse
        private void pianoControl_KeyDown(object sender, KeyEventArgs e)
        {
            pianoControl.PressPianoKey(e.KeyCode);
            base.OnKeyDown(e);
        }

        // When the user releases a Key by Mouse
        private void pianoControl_KeyUp(object sender, KeyEventArgs e)
        {
            pianoControl.ReleasePianoKey(e.KeyCode);
            base.OnKeyUp(e);
        }

        // For Closing the Forms
        private void Piano_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }
       
    }
}
