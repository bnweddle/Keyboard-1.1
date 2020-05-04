/* Author: Bethany Weddle
 * Note.cs
 * */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class Note
    {
        /// <summary>
        /// Set the Note Position to be Drawn
        /// </summary>
        public Point NotePosition { get; set; }

        /// <summary>
        /// The time when the note is pressed
        /// </summary>
        public DateTime NoteStart { get; set; }

        /// <summary>
        /// The Type of Note played
        /// </summary>
        public Timing NoteTime { get; set; }

        /// <summary>
        /// The noteID of the Note
        /// </summary>
        public int NoteID { get; set; }

        /// <summary>
        /// The Key the sheet music is written in: Sharp or Flat
        /// </summary>
        public Chromatic MusicKey { get; set; }

        /// <summary>
        /// Create in the Piano to the passed to Measure?
        /// </summary>
        /// <param name="ID">the noteID</param>
        /// <param name="start">the time when the Note was pressed</param>
        /// <param name="time">the Timing of the Note</param>
        /// <param name="music">The key the music is in</param>
        public Note(int ID, DateTime start, Timing time, Chromatic music)
        {
            NoteID = ID;
            NoteStart = start;
            NoteTime = time;
            MusicKey = music;
        }

        /// <summary>
        /// Created in Measure to be used in SheetMusic to Draw the Note?
        /// </summary>
        /// <param name="position">Where the note should be positioned X and Y</param>
        /// <param name="time">The Timing of the Note</param>
        public Note(Point position, Timing time)
        {
            NotePosition = position;
            NoteTime = time;
        }

    }
}
