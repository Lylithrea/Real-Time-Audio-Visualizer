using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Tooling
{

    public class FrequencyBander
    {

        private static float previousHz = 0;
        private static float HzIntensity = 0;
        private static List<float> HzIntensities = new List<float>();


        public static void FrequencyBands()
        {
            int count = 0;

            previousHz = 0;

            for (int i = 0; i < Tooling.Base.bands; i++)
            {
                float average = 0;

                float hz = (Mathf.Pow(2, (i / 6.3f))) * 2;

                for (float j = previousHz; j < hz; j++)
                {
                    if (count > Tooling.Base.samples - 1)
                    {
                        //Debug.Log("Bigger than samples");
                        break;
                    }
                    //average += Tooling.Base.audioSpectrum[count] * ((count + 1) / 2);
                    //float normalizer = Mathf.Pow(2, (-count / 6.3f));
                    average += Tooling.Base.audioSpectrum[count] * ((count + 4) / 3);
                    count++;
                }
                //Debug.Log("Previous Hz: " + previousHz + " Current Hz: " + hz + " with count: " + count);
                previousHz = hz;


                if (count != 0)
                {
                    average /= count;
                }



                if (average <= 0.001f)
                {
                    average = 0;
                }
                average *= 500;
                HzIntensities.Insert(0, average);
                //if the array list is longer than the requested amount, it will delete the outdated data
                if (HzIntensities.Count > 512)
                {
                    HzIntensities.RemoveRange(512, HzIntensities.Count - 513);
                }
                for (int j = 0; j < HzIntensities.Count; j++)
                {
                    HzIntensity += HzIntensities[j];
                }
                HzIntensity /= HzIntensities.Count;
                float final = average * Mathf.Clamp01(Mathf.Pow(average, 2) / (HzIntensity * 10));
                //float final = average * (Mathf.Clamp01(Mathf.Pow(average, 2)) *( average / HzIntensity)) / (HzIntensity * 10);
                //final = average;
                Tooling.Base._freqBand[i] = final;
            }

        }
    }
}
