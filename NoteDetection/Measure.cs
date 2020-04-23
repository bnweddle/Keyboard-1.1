using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    /// <summary>
    /// How to think about getting and setting Note Position?
    /// </summary>
    public class Measure
    {
        /// <summary>
        /// The Width of each Measure, 16 sixteenth notes = whole note!! That's crazy width
        /// </summary>
        public int Width { get; set; } = 925;

        /// <summary>
        /// How many Milliseconds (counts) are on a full measure
        /// /// </summary>
        public long Count { get; set; } = 4000;

        /// <summary>
        /// List of Notes per Measure to draw
        /// </summary>
        public List<Note> FullMeasure { get; set; }

        /// <summary>
        /// Queue of Played Notes
        /// </summary>
        public Queue<Note> PlayedNotes { get; set; }

        private Note current;
        private long measureCount;

        public bool CompleteMeasure()
        {
            measureCount = 0;

            while(measureCount <= Count)
            {
                if ((measureCount += PeakTime()) == Count)
                {
                    break;
                }
                else if((measureCount += PeakTime()) < Count)
                {
                    measureCount += DequeueTime();
                }
                else // it is greater and needs to be in the next measure
                {
                    break;
                }
            }

            return true;
            // If true need to draw vertical line at and begin new measure
        }

        private long EstimateCount(Timing time)
        {
            long count = 0;
            switch (time)
            {
                case Timing.Sixteenth:
                    count = 250;
                    break;
                case Timing.ThirdSixteen:
                    count = 375;
                    break;
                case Timing.Eighth:
                    count = 500;
                    break;
                case Timing.ThirdEigth:
                    count = 750;
                    break;
                case Timing.Quarter:
                    count = 1000;
                    break;
                case Timing.ThirdQuart:
                    count = 1500;
                    break;
                case Timing.Half:
                    count = 2000;
                    break;
                case Timing.ThirdHalf:
                    count = 3000;
                    break;
                case Timing.Whole:
                    count = 4000;
                    break;
            }

            return count;
        }

        private long PeakTime()
        {
            current = PlayedNotes.Peek();
            return EstimateCount(current.NoteTime);
        }

        private long DequeueTime()
        {
            current = PlayedNotes.Dequeue();
            return EstimateCount(current.NoteTime);
        }

    }
}
