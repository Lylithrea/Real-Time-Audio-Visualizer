using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Tooling
{

    public class BeatDetector
    {
        public void OnApplicationQuit()
        {
            //Reset delegate, or else it will throw exception
            Tooling.Base.onBeat = null;
        }

        private static float averageBeatVol = 5;
        private static List<float> lowBeatsVol = new List<float>();
        private static float beatCooldown;
        private static float biasTimer = 0;
        private static float bpmTimer = 0;
        private static float bpsTimer = 0;
        private static List<float> beatList = new List<float>();    

        //TODO: Create dynamic bias, based on total volume of low frequency
        //based on this total volume, adjust bias accordingly
        //implement minimum check timer (instead of every beat)
        //so that if its silent the bpm will be updated.

        private static void Beat()
        {
            beatList.Insert(0,beatCooldown);
            //if the array list is longer than the requested amount, it will delete the outdated data
            if (beatList.Count > 120)
            {
                beatList.RemoveRange(120, beatList.Count - 121);
            }
            beatCooldown = 0;
            BPMCalculator();
            Base.onBeat();
        }

        public static void CheckBias()
        {
            //Add the low fequency to the array
            lowBeatsVol.Insert(0, PitchCalculator.getLowPitch());

            //if the array list is longer than the requested amount, it will delete the outdated data
            if (lowBeatsVol.Count > 75)
            {
                lowBeatsVol.RemoveRange(75, lowBeatsVol.Count- 76);
            }
            //Calculate the average volume of all the stored low frequency data
            for (int i = 0; i < lowBeatsVol.Count; i++)
            {
                averageBeatVol += lowBeatsVol[i];
            }
            averageBeatVol /= lowBeatsVol.Count;

            Base.bias = averageBeatVol;
        }



        public static void CheckBeat()
        {
            //how often the bias updates in seconds
            if (biasTimer > 0.25f)
            {
                biasTimer = 0;
                CheckBias();
            }


            //Debug.Log("Low pitch: " + PitchCalculator.getLowPitch() + " bps: " + Base.bps + " beat cooldown: " + beatCooldown + " calculated beat time: " + 60 / Base.bps / 2);

            //BPS
            //check if the currently low frequency is higher than the average frequency volume / bias
            if (PitchCalculator.getLowPitch() > averageBeatVol)
            {
                //if so insert the frequency again, but with lower impact
                float beatTime = beatCooldown;
                Mathf.Clamp(beatTime, 0, 0.5f);
                beatTime *= 2;
                beatTime = (-1 * Mathf.Pow(beatTime, 3) + 1);
                Mathf.Clamp01(beatTime);
                lowBeatsVol.Insert(0, PitchCalculator.getLowPitch() * beatTime);

                if (beatCooldown > 0.15f)
                {

                    Beat();
                }

            }

            //update timers
            beatCooldown += Time.deltaTime;
            biasTimer += Time.deltaTime;
        }



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
