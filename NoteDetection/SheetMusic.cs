/* Author: Bethany Weddle
 * Class: SheetMusic.cs 
 * 
 TO DO: 
 * 1. Change how Keyboard Keys work (LIMITATION)      - Ask Professor
 * 2. Think about Rests (time between pressed notes)  - Get Idea from Professor 
 * 3. Implement Measures checking with Time Signature - 2 & 3 would go hand-in-hand 
 * 4. Fix Scrolling Off the Form Issue                - Show Professor
 *    (Slightly better)
 * 5. Two Notes right beside each other are pressed  
 * 6. If multiple eight or sixteenth notes are pressed only draw one curly end
 * 7. Have offset problem 
 *    
 * Extra to Think About for future:
 * 1. How to implement Beams when multiple Eighth/Sixteenth Notes are pressed
 * 2. How to read and play back what was created
 * 3. How to print PDF
 * */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Collections.Generic;

namespace NoteDetection
{
    /// <summary>
    /// SheetMusic Class used for Displaying Lines, Symbols, Chromatics, Clefs on Form
    /// </summary>
    public partial class SheetMusic : Form
    {
        // For the font import
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        // private variables for scrolling and drawing sheet music lines
        private int staffHeight = 15;
        private int staffWidth = 900;
        private int scrollWidth = 900;
        private int scroll = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public SheetMusic()
        {
            InitializeComponent();
            this.AutoScroll = true;
            this.ResizeRedraw = true;
            ImportFont();
        }

        // For adding the font the the Font family
        FontFamily ff;
        Font font;

        // Symbols for upper and lower clefs
        Symbol treble = new Symbol("\uD834\uDD1E", 75, 55, 175);
        Symbol bass = new Symbol("\uD834\uDD22", 75, 50, 330);
        Symbol upperTreble = new Symbol("\uD834\uDD1E", 75, 55, 70);
        Symbol lowerBass = new Symbol("\uD834\uDD22", 75, 50, 435);

        // Lists for drawing Right and Left Hand Notes
        List<Symbol> DrawingRightNotes = new List<Symbol>();
        List<Symbol> DrawingLeftNotes = new List<Symbol>();

        // Paint graphics
        Graphics g;

        // The hand offset for the special symbols: dot, sharp, flat
        int handOffsetX;
        int handOffsetY;

        // The chromatic value of the Notes
        Chromatic chromValue = Chromatic.Natural;

        /// <summary>
        ///  Used for Repainting the Form when new Note is pressed
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        /// <summary>
        /// Updates the Form by Paining the notes as they are played.
        /// </summary>
        /// <param name="off">The spacing between notes: X position</param>
        /// <param name="third">indicates whether the note needs a dot beside it</param>
        /// <param name="position">the Y position for the specific noteID </param>
        public void UpdatePaint(int off, bool third, double position)
        {
            // Handle the auto scrolling while playing
            scrollWidth += 40;
            staffWidth += 40;
            scroll = off - this.Size.Width + 100;

            this.AutoScrollMinSize = new Size(scrollWidth, this.Size.Height - 100);
            this.AutoScrollPosition = new Point(scroll, 0);
            Symbol symbol;

            if (Global.Handy == Hand.Right)
            {
                // All right hand notes
                symbol = new Symbol(Global.Symbol, 65, off, (float)position);
                DrawingRightNotes.Add(symbol);
                handOffsetX = 0;
                handOffsetY = 0;
            }
            else // Left Hand
            {
                // if left hand is a third note, dot needs to be shifted slightly
                if (third) handOffsetX = 15; else handOffsetX = 18;
                handOffsetY = 70;

                // Whole note must be sized differently
                if (Global.Time == Timing.Whole)
                {
                    symbol = new Symbol(Global.Image, off + 20, (float)position, 24, 15);
                    DrawingLeftNotes.Add(symbol);
                }
                else
                {
                    // All other left hand notes
                    symbol = new Symbol(Global.Image, off + 20, (float)position, 20, 60);
                    DrawingLeftNotes.Add(symbol);
                }
            }


            if (third)
            {   
                Symbol s = new Symbol("\uD834\uDD58", 25, symbol.X + 30 - handOffsetX, symbol.Y + 48 - handOffsetY);
                DrawingRightNotes.Add(s);
                handOffsetX += 5; // reset back to normal, so it doesn't effect other offsets
            }
            if (chromValue == Chromatic.Sharp)
            {
                if (third) handOffsetX -= 2;
                // set the Sharp symbol position relative to the hand offset
                Symbol s = new Symbol(Global.Chromatic, 20, symbol.X - handOffsetX, symbol.Y + 70 - handOffsetY);
                DrawingRightNotes.Add(s);
            }
            if (chromValue == Chromatic.Flat)
            {
                if (third) handOffsetX -= 2;
                // set the Flat symbol position relative to the hand offset
                Symbol s = new Symbol(Global.Chromatic, 20, symbol.X - handOffsetX - 1, symbol.Y + 70 - handOffsetY);
                DrawingRightNotes.Add(s);
            }
            Invalidate();
        }

        /// <summary>
        /// Sets what the Chromatic Value for each notes
        /// </summary>
        /// <param name="isChromatic">indicates if note is black</param>
        /// <param name="type">the Chromatic Value of the note</param>
        /// <returns>whether key pressed needs Chromatic Unicode</returns>
        public bool SetChromatic(bool isChromatic, Chromatic type)
        {
            this.chromValue = type;
            if (isChromatic)
            {
                if (type == Chromatic.Sharp)
                    this.chromValue = Chromatic.Sharp;
                else
                    this.chromValue = Chromatic.Flat;
            }
            else
            {
                this.chromValue = Chromatic.Natural;
            }

            return isChromatic;
        }

        /// <summary>
        /// Paints List of Notes as played with Sheet Music Lines and Clefs with Default 
        /// Time Signature
        /// </summary>
        private void SheetMusic_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
            g.SmoothingMode = SmoothingMode.HighQuality;
            DrawLines(g);

            for (int i = 0; i < DrawingRightNotes.Count; i++)
            {
                DrawingRightNotes[i].DrawSymbol(g, font, ff);
            }

            for(int i = 0; i < DrawingLeftNotes.Count; i++)
            {
                DrawingLeftNotes[i].DrawSymbol(g);
            }

            treble.DrawSymbol(g, font, ff, 5, 25);
            bass.DrawSymbol(g, font, ff, 10, 30);
            upperTreble.DrawSymbol(g, font, ff, 5, 25);
            lowerBass.DrawSymbol(g, font, ff, 10, 30);

        }

        /// <summary>
        /// Imports the Symbola font to allow Musical Unicode Symbols to be drawn
        /// </summary>
        private void ImportFont()
        {
            // Create the byte array and get its length
            byte[] fontArray = Properties.Resources.Symbola;
            int dataLength = Properties.Resources.Symbola.Length;


            // ASSIGN MEMORY AND COPY  BYTE[] ON THAT MEMORY ADDRESS
            IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontArray, 0, ptrData, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();
            //PASS THE FONT TO THE  PRIVATEFONTCOLLECTION OBJECT
            pfc.AddMemoryFont(ptrData, dataLength);

            //FREE THE  "UNSAFE" MEMORY
            Marshal.FreeCoTaskMem(ptrData);

            ff = pfc.Families[0];
            font = new Font(ff, 15f, FontStyle.Bold);
        }

        /// <summary>
        /// Draws the Lines on the Form for the Sheet Music
        /// </summary>
        /// <param name="g">The paint graphics</param>
        public void DrawLines(Graphics g)
        {
            int i;
            // draw some staff lines, 900 will need to change as user is playing, want to scroll with sheet music as user plays as well
            for (i = 0; i < 4; i++)
                g.DrawLine(Pens.White, 0, i * staffHeight, staffWidth, i * staffHeight); // White space for extra room
            for (; i < 13; i++)
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, staffWidth, i * staffHeight); // High notes
            for (; i < 18; i++)
                g.DrawLine(Pens.Black, 0, i * staffHeight, staffWidth, i * staffHeight); // Middle treble clef range
            i = 18;
            g.DrawLine(Pens.Wheat, 0, i * staffHeight, staffWidth, i * staffHeight); // Middle C
            i++;
            for (; i < 23; i++)
                g.DrawLine(Pens.White, 0, i * staffHeight, staffWidth, i * staffHeight); // Middle notes
            for (; i < 28; i++)
                g.DrawLine(Pens.Black, 0, i * staffHeight, staffWidth, i * staffHeight); // Middle bass clef range
            for (; i < 34; i++)
                g.DrawLine(Pens.Wheat, 0, i * staffHeight, staffWidth, i * staffHeight); // Low notes
        }
    }
}
