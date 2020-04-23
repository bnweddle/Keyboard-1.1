/* Author: Bethany Weddle
 * Class: Keys.cs
 * */

namespace NoteDetection
{
    /// <summary>
    /// Keys class used for setting and getting the Y Position for the Sheet Music
    /// </summary>
    public class Keys
    {
        /// <summary>
        /// The Number of Black Keys and their Indexes on the Piano
        /// </summary>
        private int[] blackKeys = new int[36]
        {
            1, 4, 6, 9, 11, 13, 16, 18, 21, 23, 25, 28, 30, 33, 35, 37, 40, 42, 45, 47, 49,
            52, 54, 57, 59, 61, 64, 66, 69, 71, 73, 76, 78, 81, 83, 85
        };

        /// <summary>
        /// The black keys relative to the 52 white keys
        /// </summary>
        private int[] black52 = new int[36]
        {
            0, 2, 3, 5, 6, 7, 9, 10, 12, 13, 14, 16, 17, 19, 20, 21, 23, 24, 26, 27, 28, 30,
            31, 33, 34 ,35, 37, 38, 40, 41, 42, 44, 45 ,47, 48, 49
        };


        /// <summary>
        /// The Number of White Keys and their Indexes on the Piano
        /// </summary>
        private int[] whiteKeys = new int[52]
        {
            0, 2, 3, 5, 7, 8, 10, 12, 14, 15, 17, 19, 20, 22, 24, 26, 27, 29, 31, 32, 34, 36,
            38, 39, 41, 43, 44, 46, 48, 50, 51, 53, 55, 56, 58, 60, 62, 63, 65, 67, 68, 70, 72,
            74, 75, 77, 79, 80, 82, 84, 86, 87
        };

        /// <summary>
        /// The Y Position for all Keys
        /// </summary>
        private double[] positions = new double[88];

        /// <summary>
        /// Checks which Black Key was pressed
        /// </summary>
        /// <param name="noteID">The key that was pressed</param>
        /// <param name="chrom">What the Chromatic value is: sharp or flat</param>
        /// <returns>Black Note Index</returns>
        public int BlackKeyPress(int noteID, out bool chrom)
        {
            // Need to check if any black of the black keys values equal noteID
            for (int i = 0; i < blackKeys.Length; i++)
            {
                if (blackKeys[i] == noteID - 21)
                {
                    chrom = true;
                    return black52[i]; //CHANGED
                }
            }

            chrom = false;
            return -1;
        }

        /// <summary>
        /// Checks which of the White Keys was pressed
        /// </summary>
        /// <param name="noteID">the key that was pressed</param>
        /// <param name="chrom">What the Chromatic value is: sharp or flat</param>
        /// <returns>white key number</returns>
        public int WhiteKeyPress(int noteID, out bool chrom)
        {
            for (int i = 0; i < whiteKeys.Length; i++)
            {
                if (whiteKeys[i] == noteID - 21)
                {
                    chrom = false;
                    return i;
                }
            }

            chrom = true;
            return -1;
        }

        /// <summary>
        /// Sets the Positions for all Keys
        /// </summary>
        /// <param name="blackPressed">The index of the black notes presseed</param>
        /// <param name="whitePressed">The current pressed white note</param>
        /// <param name="type">the Chromatic type of the note</param>
        /// <param name="chrom">If the key is black</param>
        public void SetPositions(int blackPressed, int whitePressed, Chromatic type, bool chrom)
        {
            
            double index = 0;
            for (int i = 0; i < positions.Length; i++)
            {
                // start position will need to change for the left hand 
                positions[i] = 425 - index;

                if (whitePressed != -1)
                {
                    index = 0;
                    index = whitePressed * 7.5;
                }

                if (chrom)
                {
                    if (type == Chromatic.Sharp)
                    {
                        index = blackPressed * 7.5;
                    }
                    else if (type == Chromatic.Flat)
                    {
                        index = (blackPressed - 1) * 7.5;
                    }
                }

                if (positions[i] <= 252.5)
                {
                    positions[i] -= 60; // move to the g clef
                    Global.Handy = Hand.Right;
                }
                else
                {
                    positions[i] += 70; // adjust for note height difference
                    Global.Handy = Hand.Left;
                }
            }
        }

        /// <summary>
        /// Gets the position of the specific pressed Note
        /// </summary>
        /// <param name="noteID">The note that was pressed</param>
        /// <returns>position of the pressed note</returns>
        public double GetPosition(int noteID)
        {
            return positions[noteID - 21];
        }
    }
}
