/* Author: Bethany Weddle
 * Class: Piano.cs
 * Used PianoControl from MidiKit on Github with Free Software License
 * */
using System;
using System.Collections.Generic;
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

        // TimeSpans for rests
        TimeSpan rightRest = new TimeSpan();
        TimeSpan leftRest = new TimeSpan();
        
        // Measures and Rests
        int leftHand, rightHand;
        int offset = 75;
        bool startPlaying = false;
        bool tickingLeft = false;
        bool tickingRight = false;
        int measureCount;


        DateTime startTime;
        System.Timers.Timer metronome;

        // Note objects
        NoteEstimator noteEstimator;
        Drawn drawn = new Drawn();
        Keys keys = new Keys();
        Measure measure = new Measure();
        SheetMusic sheetForm;

        // private variables passed between Forms
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
            
            measureCount++;
            // The offset is getting way off

            if (rightHand == 0)
            {
                if (!tickingRight)
                {
                    tickingRight = true;
                }
                else
                {
                    // Only displays quarter rests
                    rightRest = rightRest.Add(new TimeSpan(noteEstimator.SixteenthCount));
                    System.Diagnostics.Debug.WriteLine($"{rightRest.Ticks} right rest before");
                    if (rightRest.Ticks >= (int)noteEstimator.QuartCount)
                    {
                        offset += 45;
                        sheetForm.ScrollWidth += 45;
                        sheetForm.StaffWidth += 45;
                        sheetForm.Rests.Add(new Symbol("\uD834\uDD3D", 60, offset, 150));
                        rightRest = rightRest.Subtract(new TimeSpan(noteEstimator.QuartCount));
                    }

                    System.Diagnostics.Debug.WriteLine($"{rightRest.Ticks} right rest after");                 
                }            

            }      
            if(rightHand != 0 && tickingRight)
            {
                tickingRight = false;
            }

            if (leftHand == 0)
            {
                if (!tickingLeft)
                {
                    tickingLeft = true;
                }
                else
                {
                    leftRest = leftRest.Add(new TimeSpan(noteEstimator.SixteenthCount));

                    if (leftRest.Ticks >= (int)noteEstimator.QuartCount)
                    {
                        offset += 45;
                        sheetForm.ScrollWidth += 45;
                        sheetForm.StaffWidth += 45;
                        sheetForm.Rests.Add(new Symbol("\uD834\uDD3D", 60, offset, 300));
                        leftRest = leftRest.Subtract(new TimeSpan(noteEstimator.QuartCount));
                    }
                }
            }
            if (leftHand != 0 && tickingLeft)
            {
                tickingLeft = false;
            }


            if (measureCount > 16)
            {
                sheetForm.MeasurePositions.Add(measure.Width);
                measure.Width += 900;
                measureCount = 0;
            }
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

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));

            if (Global.Handy == Hand.Left)
            {
                leftHand++;
            }
            else if (Global.Handy == Hand.Right)
            {
                rightHand++;
            }            

            System.Diagnostics.Debug.WriteLine($"{leftHand } lefty");
            System.Diagnostics.Debug.WriteLine($"{rightHand } righty");

            if (!startPlaying)
            {
                metronome.Start();
                startPlaying = true;
            }

            offset += 45;
        }

        // For when the Keyboard Note or Mouse Note is released
        private void PianoControl_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {
            if (Global.Handy == Hand.Left)
            {
                leftHand--;
            }
            else if (Global.Handy == Hand.Right)
            {
                rightHand--;
            }

            if (leftHand < 0) leftHand = 0;
            if (rightHand < 0) rightHand = 0;

            // Setting the Positions
            blackPressed = keys.BlackKeyPress(e.NoteID, out chrom);
            whitePressed = keys.WhiteKeyPress(e.NoteID, out chrom);

            //Chromatic oldValue = chromatic;
            //int shiftX = keys.ChangePosition(oldY, newY, numberPlayed, chrom, oldValue, out chromatic);

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

            int shiftX = 0;  // Get rid later!!!
            sheetForm.UpdatePaint(offset, thirds, oldY);

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
