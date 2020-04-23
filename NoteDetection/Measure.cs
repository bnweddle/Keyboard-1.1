using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDetection
{
    public class Measure
    {
        /// <summary>
        /// The Width of each Measure
        /// </summary>
        public int Width { get; set; } = 500;

        /// <summary>
        /// How many Milliseconds (counts) are on a full measure
        /// /// </summary>
        public long Count { get; set; } = 4000;

        /// <summary>
        /// List of Notes per Measure to draw for left hand
        /// </summary>
        public List<Note> LeftFullMeasure { get; set; }

        /// <summary>
        /// List of Notes per Measure to draw for right hand
        /// </summary>
        public List<Note> RightFullMeasure { get; set; }

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
            }

            return true;
            // If true need to draw vertical line at 
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
                case Timing.ThirdWhole:
                    count = 6000;
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
