/* Author: Bethany Weddle
 * Class: NoteEstimator.cs
 * Got Help from Advisor Nathan Bean in implementing
*/

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

        /// <summary>
        ///  Stores the thresholds based on the BPM retrieved from Start Form
        /// </summary>
        /// <param name="bpm">Beats per Minute from Start Form</param>
        public NoteEstimator(int bpm)
        {
            thresholds = new long[10];
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
            thresholds[(int)Timing.ThirdWhole] = (long)((minute / bpm) * 5);

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
            if (duration < thresholds[8])
            {
                return Timing.Whole;
            }
            return Timing.ThirdWhole;
        }

    }
}
