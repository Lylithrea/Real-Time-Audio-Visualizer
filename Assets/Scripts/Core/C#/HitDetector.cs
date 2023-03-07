using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Tooling
{

    public class HitDetector
    {


        private static float subBias = 0;
        private static float bassBias = 0;
        private static float midLowBias = 0;
        private static List<float> subBeatsVol = new List<float>();
        private static List<float> bassBeatsVol = new List<float>();
        private static List<float> midLowBeatsVol = new List<float>();
        private static float subTimer;
        private static float bassTimer;
        private static float midLowTimer;
        private static float biasTimer = 0;
        private static List<float> beatList = new List<float>();

        //TODO: Create dynamic bias, based on total volume of low frequency
        //based on this total volume, adjust bias accordingly
        //implement minimum check timer (instead of every beat)
        //so that if its silent the bpm will be updated.





        public static void CheckHits()
        {

            //how often the bias updates in seconds
            if (biasTimer > 0.1f)
            {
                biasTimer = 0;
                checkSubBias();
                checkBassBias();
                checkMidLowBias();
            }
            checkSub();
            checkBass();
            checkMidLow();

            biasTimer += Time.deltaTime;
        }


        private static float getDeltaTime(float timer)
        {
            float beatTime = timer;
            Mathf.Clamp(beatTime, 0, 0.5f);
            beatTime *= 2;
            beatTime = (-1 * Mathf.Pow(beatTime, 3) + 1);
            Mathf.Clamp01(beatTime);
            return beatTime;
        }

        #region midLow

        private static void checkMidLow()
        {

            if (PitchCalculator.getLowHighPitch() > midLowBias)
            {
                midLowBeatsVol.Insert(0, PitchCalculator.getLowHighPitch()  * getDeltaTime(midLowTimer));
                if (midLowTimer > 0.15f)
                {
                    midLowHit();
                }

            }

            midLowTimer += Time.deltaTime;
        }

        private static void midLowHit()
        {
            midLowTimer = 0;
            Base.onLowMid();
        }

        public static void checkMidLowBias()
        {
            //Add the low fequency to the array
            midLowBeatsVol.Insert(0, PitchCalculator.getLowHighPitch());
            midLowBias = 0;
            //if the array list is longer than the requested amount, it will delete the outdated data
            if (midLowBeatsVol.Count > 150)
            {
                midLowBeatsVol.RemoveRange(150, midLowBeatsVol.Count - 151);
            }
            //Calculate the average volume of all the stored low frequency data
            for (int i = 0; i < midLowBeatsVol.Count; i++)
            {
                if (!float.IsNaN(midLowBeatsVol[i]))
                {
                    midLowBias += midLowBeatsVol[i];
                }

            }
            midLowBias /= midLowBeatsVol.Count;

        }

        #endregion


        #region bass
        private static void checkBass()
        {

            if (PitchCalculator.getLowMidPitch() > bassBias)
            {
                bassBeatsVol.Insert(0, PitchCalculator.getLowMidPitch() * getDeltaTime(bassTimer));

                if (bassTimer > 0.15f)
                {
                    bassHit();
                }

            }

            bassTimer += Time.deltaTime;
        }

        private static void bassHit()
        {
            bassTimer = 0;
            Base.onBass();
        }

        public static void checkBassBias()
        {
            //Add the low fequency to the array
            bassBeatsVol.Insert(0, PitchCalculator.getLowMidPitch());
            bassBias = 0;
            //if the array list is longer than the requested amount, it will delete the outdated data
            if (bassBeatsVol.Count > 150)
            {
                bassBeatsVol.RemoveRange(150, bassBeatsVol.Count - 151);
            }
            //Calculate the average volume of all the stored low frequency data
            for (int i = 0; i < bassBeatsVol.Count; i++)
            {
                if (!float.IsNaN(bassBeatsVol[i]))
                {
                    bassBias += bassBeatsVol[i];
                }

            }
            bassBias /= bassBeatsVol.Count;

        }

        #endregion

        #region sub
        private static void checkSub()
        {
            if (PitchCalculator.getLowLowPitch() > subBias)
            {
                subBeatsVol.Insert(0, PitchCalculator.getLowLowPitch() * getDeltaTime(subTimer));

                if (subTimer > 0.15f)
                {
                    subHit();
                }

            }

            subTimer += Time.deltaTime;
        }

        private static void subHit()
        {
            subTimer = 0;
            Base.onSub();
        }

        public static void checkSubBias()
        {
            //Add the low fequency to the array
            subBeatsVol.Insert(0, PitchCalculator.getLowLowPitch());
            subBias = 0;
            //if the array list is longer than the requested amount, it will delete the outdated data
            if (subBeatsVol.Count > 150)
            {
                subBeatsVol.RemoveRange(150, subBeatsVol.Count - 151);
            }
            //Calculate the average volume of all the stored low frequency data
            for (int i = 0; i < subBeatsVol.Count; i++)
            {
                if (!float.IsNaN(subBeatsVol[i]))
                {
                    subBias += subBeatsVol[i];
                }

            }
            subBias /= subBeatsVol.Count;

            if (!float.IsNaN(subBias) && subBias > 0)
            {
                Base.bias = subBias;
            }
        }

        #endregion







        /// <summary>
        /// Calculate bpm based on time
        /// </summary>
        private static void BPMCalculator()
        {
            float totalTime = 0;
            for (int i = 0; i < beatList.Count; i++)
            {
                totalTime += beatList[i];
            }
            totalTime /= beatList.Count;
            //bpm is a longer range that calculates the consistent time
            Base.bpm = 60 / totalTime;

        }

    }
}
