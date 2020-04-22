/* Author: Bethany Weddle
 * Class: Note.cs
 * */
using System.Drawing;

namespace NoteDetection
{
    /// <summary>
    /// Enum for which Hand is used for using Symbols
    /// </summary>
    public enum Hand
    {
        Right,
        Left
    }

    /// <summary>
    /// Enum for setting the Unicode/Image for each type of Note
    /// </summary>
    public enum Timing
    {
        Sixteenth = 0,   // "\uD834\uDD61"
        ThirdSixteen,
        Eighth,          // "\uD834\uDD60"
        ThirdEigth,
        Quarter,         // "\uD834\uDD5F"
        ThirdQuart,      // Need a dot beside it
        Half,            // "\uD834\uDD5E"
        ThirdHalf,       // Need a dot beside it
        Whole,            // "\uD834\uDD5D"
        ThirdWhole

    };

    /// <summary>
    /// Enum for setting the Special Unicode for the Black Keys 
    /// </summary>
    public enum Chromatic
    {
        Flat,      // "\u266D"
        Sharp,     // "\u266F"
        Natural    // "\u266E"
    };

    /// <summary>
    /// Note class for getting the Symbols for drawing on Sheet Music
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Gets the Note Unicode Symbol
        /// </summary>
        /// <param name="symbol">The Timing of the played Note</param>
        /// <returns>Unicode symbol for note pressed</returns>
        public string GetNoteSymbol(Timing symbol)
        {
            string unicode = "";
            switch(symbol)
            {
                case Timing.Sixteenth:
                    unicode = "\uD834\uDD61";
                    break;
                case Timing.ThirdSixteen:
                    unicode = "\uD834\uDD61";
                    break;
                case Timing.Eighth:
                    unicode = "\uD834\uDD60";
                    break;
                case Timing.ThirdEigth:
                    unicode = "\uD834\uDD60";
                    break;
                case Timing.Quarter:
                    unicode = "\uD834\uDD5F";
                    break;
                case Timing.ThirdQuart:
                    unicode = "\uD834\uDD5F"; // Need a dot beside it
                    break;
                case Timing.Half:
                    unicode = "\uD834\uDD5E";
                    break;
                case Timing.ThirdHalf:
                    unicode = "\uD834\uDD5E"; // Need a dot beside it
                    break;
                case Timing.Whole:
                    unicode = "\uD834\uDD5D";
                    break;
                case Timing.ThirdWhole:
                    unicode = "\uD834\uDD5D";
                    break;
            }

            return unicode;
        }

        /// <summary>
        /// Gets the Chromatic Unicode for the Note
        /// </summary>
        /// <param name="symbol">The Chromatic value of the pressed note</param>
        /// <returns>The Unicode for the pressed note</returns>
        public string GetChromaticSymbol(Chromatic symbol)
        {
            string unicode = "";
            switch (symbol)
            {
                case Chromatic.Natural:
                    unicode = "\u266E";
                    break;
                case Chromatic.Flat:
                    unicode = "\u266D";
                    break;
                case Chromatic.Sharp:
                    unicode = "\u266F";
                    break;
            }
            return unicode;
        }

        /// <summary>
        /// Gets the Image for the Left Hand notes
        /// </summary>
        /// <param name="symbol">The Timing of the Pressed Note</param>
        /// <param name="time">To Set the Global Time</param>
        /// <returns>The Image of the Pressed Note</returns>
        public Image GetImage(Timing symbol, out Timing time)
        {
            Image image = null;
            time = Timing.Quarter;
            switch (symbol)
            {
                case Timing.Sixteenth:
                    time = Timing.Sixteenth;
                    image = Properties.Resources.leftSixteen;
                    break;
                case Timing.ThirdSixteen:
                    time = Timing.ThirdSixteen;
                    image = Properties.Resources.leftSixteen;
                    break;
                case Timing.Eighth:
                    time = Timing.Eighth;
                    image = Properties.Resources.leftEight;
                    break;
                case Timing.ThirdEigth:
                    time = Timing.ThirdEigth;
                    image = Properties.Resources.leftEight;
                    break;
                case Timing.Quarter:
                    time = Timing.Quarter;
                    image = Properties.Resources.leftFour;
                    break;
                case Timing.ThirdQuart:
                    time = Timing.ThirdQuart;
                    image = Properties.Resources.leftFour; // Need a dot beside it
                    break;
                case Timing.Half:
                    time = Timing.Half;
                    image = Properties.Resources.leftHalf;
                    break;
                case Timing.ThirdHalf:
                    time = Timing.ThirdHalf;
                    image = Properties.Resources.leftHalf; // Need a dot beside it
                    break;
                case Timing.Whole:
                    time = Timing.Whole;
                    image = Properties.Resources.whole; // looks a little different from right whole note
                    break;
                case Timing.ThirdWhole:
                    time = Timing.ThirdWhole;
                    image = Properties.Resources.whole; 
                    break;
            }
            return image;
        }
    }
}
