/* Author: Bethany Weddle
 * Class: Global.cs
 * */
using System;
using System.Drawing;

namespace NoteDetection
{
    /// <summary>
    /// The Global Class to share variables between all classes and forms
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// The Unicode Symbols for the Right Hand Notes
        /// </summary>
        public static string Symbol { get; set; }

        /// <summary>
        /// The Image Symbols for the Left Hand Notes
        /// </summary>
        public static Image Image { get; set; }

        /// <summary>
        /// The Chromatic Unicode for the Sharps and Flats
        /// </summary>
        public static string Chromatic { get; set; }

        /// <summary>
        /// The Hand enum for Left and Right
        /// </summary>
        public static Hand Handy { get; set; }

        /// <summary>
        /// The Time enum, mostly used for checking Third Notes
        /// </summary>
        public static Timing Time { get; set; }

        /// <summary>
        /// Round to the nearest 1000s, 100s, 10s. Using to Approximate the time for the Beats Per Minute
        /// Found resource online at
        /// https://stackoverflow.com/questions/13153616/how-to-round-a-integer-to-the-close-hundred
        /// </summary>
        /// <param name="i">the value to round</param>
        /// <param name="nearest">the multiple of ten to round closest to</param>
        /// <returns>the rounded new value</returns>
        public static long Round(this long value, int nearest)
        {
            if (nearest <= 0 || nearest % 10 != 0)
                throw new ArgumentOutOfRangeException("nearest", "Must round to a positive multiple of 10");

            return (value + 5 * nearest / 10) / nearest * nearest;
        }

    }
}
