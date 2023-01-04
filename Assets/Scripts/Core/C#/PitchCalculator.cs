using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Tooling
{

    public static class PitchCalculator
    {
        public static float getPitch(Pass pass)
        {
            switch (pass)
            {
                case Pass.low:
                    return getLowPitch();
                case Pass.mid:
                    return getMidPitch();
                case Pass.high:
                    return getHighPitch();

                case Pass.lowlow:
                    return getLowLowPitch();
                case Pass.lowmid:
                    return getLowMidPitch();
                case Pass.lowhigh:
                    return getLowHighPitch();

                case Pass.midlow:
                    return getMidLowPitch();
                case Pass.midmid:
                    return getMidMidPitch();
                case Pass.midhigh:
                    return getMidHighPitch();

                case Pass.highlow:
                    return getHighLowPitch();
                case Pass.highmid:
                    return getHighMidPitch();
                case Pass.highhigh:
                    return getHighHighPitch();

                default:
                    return getLowPitch();
            }
        }


        #region Pitches


        private static float getPitchRange(int start, int end, int max)
        {
            float edge = Mathf.Floor(Base._freqBand.Length / max);
            float counter = 0;
            for (int i = (int)(edge * start); i < edge * end; i++)
            {
                if (i < 4)
                {
                    continue;
                }
                if (i > Base._freqBand.Length)
                {
                    break;
                }
                counter += Base._freqBand[i];
            }
            return counter / edge;
        }

        public static float getLowLowPitch()
        {
            return getPitchRange(0, 1, 9);
        }

        public static float getLowMidPitch()
        {
            return getPitchRange(1, 2, 9);
        }

        public static float getLowHighPitch()
        {
            return getPitchRange(2, 3, 9);
        }

        public static float getMidLowPitch()
        {
            return getPitchRange(3, 4, 9);
        }

        public static float getMidMidPitch()
        {
            return getPitchRange(4, 5, 9);
        }

        public static float getMidHighPitch()
        {
            return getPitchRange(5, 6, 9);
        }

        public static float getHighLowPitch()
        {
            return getPitchRange(6, 7, 9);
        }

        public static float getHighMidPitch()
        {
            return getPitchRange(7, 8, 9);
        }

        public static float getHighHighPitch()
        {
            return getPitchRange(8, 9, 9);
        }


        public static float getLowPitch()
        {
            return getPitchRange(0, 1, 4);
        }

        public static float getMidPitch()
        {
            return getPitchRange(1, 3, 4);
        }

        public static float getHighPitch()
        {
            return getPitchRange(3, 4, 4);
        }


        #endregion



    }
}
