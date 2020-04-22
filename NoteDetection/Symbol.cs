/* Author: Bethany Weddle
 * Class: Symbol.cs
 * Used Images from http://linkwaregraphics.com/
 * Used Unicodes from http://www.unicode.org/charts/PDF/U1D100.pdf
 * */
using System.Drawing;

namespace NoteDetection
{
    /// <summary>
    /// Used for Creating and Drawing the Symbols on the Sheet Music
    /// </summary>
    public class Symbol
    {
        /// <summary>
        /// The size of the font
        /// </summary>
        public float Size { get; set; } = 65;

        /// <summary>
        /// the X position for the graphics
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// the Y position for the graphics
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// the width for the image
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// the height for the image
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// The image for the left hand notes
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// The brush to paint with
        /// </summary>
        private Brush noteBrush = Brushes.Black;

        /// <summary>
        /// The style of the font
        /// </summary>
        private FontStyle fontStyle = FontStyle.Regular;

        /// <summary>
        /// The Unicode symbol
        /// </summary>
        public string Unicode { get; set; }

        /// <summary>
        /// Creates a Unicode Symbol
        /// </summary>
        /// <param name="code">Unicode string</param>
        /// <param name="size">Size of the Font</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        public Symbol(string code, float size, float x, float y)
        {
            Unicode = code;
            Size = size;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Creates an Image Symbol
        /// </summary>
        /// <param name="image">The image from the Resources</param>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Width of Image</param>
        /// <param name="height">Height of Image</param>
        public Symbol(Image image, float x, float y, float width, float height)
        {
            Image = image;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Draws the Unicode Symbol
        /// </summary>
        /// <param name="g">The paint graphics</param>
        /// <param name="font">The font to use</param>
        /// <param name="ff">The fony family the font belongs to</param>
        public void DrawSymbol(Graphics g, Font font, FontFamily ff)
        {
            font = new Font(ff, this.Size, fontStyle);
            g.DrawString(this.Unicode, font, noteBrush, this.X, this.Y);
        }

        /// <summary>
        /// Draws the Image Symbol
        /// </summary>
        /// <param name="g">The paint graphics</param>
        public void DrawSymbol(Graphics g)
        {
            g.DrawImage(this.Image, this.X, this.Y, this.Width, this.Height);
        }
      
        /// <summary>
        /// Draws the Clefs
        /// </summary>
        /// <param name="g">The paint graphics</param>
        /// <param name="font">The font to use</param>
        /// <param name="ff">The font family the font belongs to</param>
        /// <param name="xOffset">The X offset to draw the Time signature</param>
        /// <param name="yOffset">The Y offset to draw the Time signature</param>
        public void DrawSymbol(Graphics g, Font font, FontFamily ff, int xOffset, int yOffset)
        {
            font = new Font(ff, this.Size, fontStyle);
            // Bass xOffset = 10 yOffset = 30
            // Treble xOffset = 5 yOffset = 25
            g.DrawString(this.Unicode, font, noteBrush, this.X, this.Y);
            g.DrawString("\uD834\uDD34", font, noteBrush, this.X + xOffset, this.Y - yOffset);
        }
    }
}
