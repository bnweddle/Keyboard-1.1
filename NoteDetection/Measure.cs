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
        /// The Width of each Measure, 16 sixteenth notes = whole note!! That's crazy width
        /// </summary>
        public int Width { get; set; } = 925;

        /// <summary>
        /// How many Milliseconds (counts) are on a full measure
        /// /// </summary>
        public long Count { get; set; } = 4000;

        /// <summary>
        /// List of Each Measure with List of Notes to Draw
        /// </summary>
        public List<List<Note>> FullMeasure { get; set; }

        /// <summary>
        /// Queue of Played Notes
        /// </summary>
        public Queue<Note> PlayedNotes { get; set; }

        private Note current;
        private long measureCount;

        public bool CompleteMeasure(Queue<Note> played)
        {
            List<Note> measure = new List<Note>();
            measureCount = 0;

            while(measureCount <= Count)
            {
                if ((measureCount += PeakTime(played)) == Count)
                {
                    measure.Add(current);
                    measureCount += DequeueTime(played);
                    break;
                }
                else if((measureCount += PeakTime(played)) < Count)
                {
                    measure.Add(current);
                    measureCount += DequeueTime(played);
                }
                else // it is greater and needs to be in the next measure
                {
                    break;
                }
            }

            FullMeasure.Add(measure);
            return true;
            // If true need to draw vertical line at and begin new measure
        }

        /// <summary>
        /// How to think about getting and setting Note Position???
        /// </summary>
        public Queue<Note> SetPositions(Keys keys)
        {
            Queue<Note> queue = new Queue<Note>();
            
            while(PlayedNotes.Count != 0)
            {
                Note note = PlayedNotes.Dequeue();
                double offsetX = note.GetSpacing(note.NoteTime);
                double y = keys.GetPosition(note.NoteID);

                // Check Note Time for how to offset X;
                // Create new Note(position, Timing) add Note to queue
                // which will in turn be used in Complete Measure
               
            }

            return queue;
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

        private long PeakTime(Queue<Note> played)
        {
            current = played.Peek();
            return EstimateCount(current.NoteTime);
        }

        private long DequeueTime(Queue<Note> played)
        {
            current = played.Dequeue();
            return EstimateCount(current.NoteTime);
        }

    }
}
