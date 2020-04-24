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
        System.Timers.Timer metronome;

        // Note objects
        NoteEstimator noteEstimator;
        Drawn drawn = new Drawn();
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

            metronome = new System.Timers.Timer(noteEstimator.SixteenthCount);
            metronome.Elapsed += OnTick;
        }

        private void OnTick(object args, System.Timers.ElapsedEventArgs e)
        {
            
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

        double oldY = 0;
        double newY = 0;
        int numberPlayed = 0;

        // For when the Keyboard Note or Mouse  Note is pressed
        private void PianoControl_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            oldTimers[e.NoteID].Start();
            
            startTime = DateTime.UtcNow;
            metronome.Start(); // HERE??

            //System.Diagnostics.Debug.WriteLine($"{e.NoteID} noteID");
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));

            offset += 45;
        }

        // For when the Keyboard Note or Mouse Note is released
        private void PianoControl_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {
            // Setting the Positions

            whitePressed = keys.WhiteKeyPress(e.NoteID, out chrom);
            blackPressed = keys.BlackKeyPress(e.NoteID, out chrom);

            Chromatic oldValue = chromatic;
            int shiftX = keys.ChangePosition(oldY, newY, numberPlayed, chrom, oldValue, out chromatic);

            keys.SetPositions(blackPressed, whitePressed, chromatic, chrom);
            
            sheetForm.SetChromatic(chrom, chromatic);
            oldY = keys.GetPosition(e.NoteID);

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



            System.Diagnostics.Debug.WriteLine($"{oldY } old Y Position");
            System.Diagnostics.Debug.WriteLine($"{newY } new Y Position");     

            

            newY = oldY;

            // Globally shared variables
            Global.Symbol = drawn.GetNoteSymbol(symbols);
            Global.Chromatic = drawn.GetChromaticSymbol(chromatic);
            Timing time;
            Global.Image = drawn.GetImage(symbols, out time);
            Global.Time = time;

            sheetForm.UpdatePaint(offset + shiftX, thirds, oldY);

            oldTimers[e.NoteID].Reset();
        }

        // When the user hits a Key by Mouse
        private void pianoControl_KeyDown(object sender, KeyEventArgs e)
        {
            pianoControl.PressPianoKey(e.KeyCode);
            numberPlayed++;
            base.OnKeyDown(e);
        }

        // When the user releases a Key by Mouse
        private void pianoControl_KeyUp(object sender, KeyEventArgs e)
        {
            pianoControl.ReleasePianoKey(e.KeyCode);
            base.OnKeyUp(e);
            numberPlayed = 0;
        }

        // For Closing the Forms
        private void Piano_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }
       
    }
}
