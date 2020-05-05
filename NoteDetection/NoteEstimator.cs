/* Author: Bethany Weddle
 * Class: NoteEstimator.cs
 * Got Help from Advisor Nathan Bean in implementing
*/

using System.Collections.Generic;

namespace NoteDetection
{
    /// <summary>
    /// Used for estimating the Note Timing based on the Note duration and BPM
    /// </summary>
    public class NoteEstimator
    {
        /// <summary>
        /// The Thresholds for the Type of Notes
        /// </summary>
        private static long[] thresholds;

        public long SixteenthCount => thresholds[0];

        public long QuarterCount => thresholds[4];

        /// <summary>
        ///  Stores the thresholds based on the BPM retrieved from Start Form
        /// </summary>
        /// <param name="bpm">Beats per Minute from Start Form</param>
        public NoteEstimator(int bpm)
        {
            thresholds = new long[9];
            int minute = 60000;

            thresholds[(int)Timing.Sixteenth] = (minute / bpm) / 3;
            thresholds[(int)Timing.ThirdSixteen] = (minute / bpm) / 2;
            thresholds[(int)Timing.Eighth] = (long)((minute / bpm) / 1.5);
            thresholds[(int)Timing.ThirdEigth] = (minute / bpm);
            thresholds[(int)Timing.Quarter] = (long)((minute / bpm) * 1.5);
            thresholds[(int)Timing.ThirdQuart] = (long)((minute / bpm) * 2);
            thresholds[(int)Timing.Half] = (minute / bpm) * 3;
            thresholds[(int)Timing.ThirdHalf] = (long)((minute / bpm) * 3.5);
            thresholds[(int)Timing.Whole] = (long)((minute / bpm) * 4.5);

        }

        public string[] GetRestSymbol(long duration)
        {
            List<string> rests = new List<string>();
            System.Diagnostics.Debug.WriteLine($"{duration} duration");

            while (duration >= thresholds[4])
            {
                rests.Add("\uD834\uDD3D");
                duration -= thresholds[4];
                System.Diagnostics.Debug.WriteLine("quarter rest");
            }
            while (duration >= thresholds[2])
            {
                rests.Add("\uD834\uDD3E"); // look at later
                duration -= thresholds[2];
                System.Diagnostics.Debug.WriteLine("eigth rest");
            }
            while (duration >= thresholds[0])
            {
                rests.Add("\uD834\uDD3E"); 
                duration -= thresholds[0];
                System.Diagnostics.Debug.WriteLine("sixteen rest");
            }

            return rests.ToArray();
        }

        public int MultipleRests(long duration)
        {
            int iterations = 0;
            duration = duration.Round(100);

            iterations = (int)(duration % thresholds[4]);

            return iterations;
        }

        /// <summary>
        /// Gets the Timing per the Duration
        /// </summary>
        /// <param name="duration">Rounded Stopwatch duration</param>
        /// <returns>Riming of the pressed Note</returns>
        public Timing GetNoteFromDuration(long duration)
        {
            if(duration < thresholds[0])
            {
                return Timing.Sixteenth;
            }
            if(duration < thresholds[1])
            {
                return Timing.ThirdSixteen;
            }
            if (duration < thresholds[2])
            {
                return Timing.Eighth;
            }
            if (duration < thresholds[3])
            {
                return Timing.ThirdEigth;
            }

            if (duration < thresholds[4])
            {
                return Timing.Quarter;
            }
            if (duration < thresholds[5])
            {
                return Timing.ThirdQuart;
            }
            if (duration < thresholds[6])
            {
                return Timing.Half;
            }
            if(duration < thresholds[7])
            {
                return Timing.ThirdHalf;
            }
            else
                return Timing.Whole;
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

    }
}
