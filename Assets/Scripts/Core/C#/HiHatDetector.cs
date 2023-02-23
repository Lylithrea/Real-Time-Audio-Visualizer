using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooling
{

    public class HiHatDetector
    {
       
        private static List<float> HighFrequencyVol = new List<float>();
        private static float HiHatBias = 5;
        private static float biasTimer = 0;
        private static float HiHatCooldown = 0;

        public static void CheckHiHatHit()
        {
            //how often the bias updates in seconds
            if (biasTimer > 0.25f)
            {
                biasTimer = 0;
                CheckBias();
            }

            if ((PitchCalculator.getHighHighPitch() + PitchCalculator.getHighMidPitch()) / 2 > HiHatBias)
            {

                HighFrequencyVol.Insert(0, (PitchCalculator.getHighHighPitch() + PitchCalculator.getHighMidPitch()) / 2);


                if (HiHatCooldown > 0.15f)
                {
                    Debug.Log("Hi-Hat Hit!");
                }
            }

            HiHatCooldown += Time.deltaTime;
            biasTimer += Time.deltaTime;
        }

        private static void CheckBias()
        {
            //Add the low fequency to the array
            HighFrequencyVol.Insert(0, (PitchCalculator.getHighHighPitch() + PitchCalculator.getHighMidPitch()) / 2);

            //if the array list is longer than the requested amount, it will delete the outdated data
            if (HighFrequencyVol.Count > 75)
            {
                HighFrequencyVol.RemoveRange(75, HighFrequencyVol.Count - 76);
            }
            //Calculate the average volume of all the stored low frequency data
            for (int i = 0; i < HighFrequencyVol.Count; i++)
            {
                HiHatBias += HighFrequencyVol[i];
            }
            HiHatBias /= HighFrequencyVol.Count;

        }


    }


}
