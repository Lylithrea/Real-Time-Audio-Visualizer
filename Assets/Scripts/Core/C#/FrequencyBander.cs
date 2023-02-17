using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooling
{

    public class FrequencyBander
    {

        private static float previousHz = 0;


        public static void FrequencyBands()
        {
            int count = 0;

            float normalizer = Mathf.Pow(2, Tooling.Base.bands - 1);

            for (int i = 0; i < Tooling.Base.bands - 1; i++)
            {
                float average = 0;

                float sampleCount = Mathf.Pow(2, i);
                float normalizedCount = sampleCount / normalizer;
                float hz = normalizedCount * Tooling.Base.samples;

                //go through all the samples, and add them together
                for (float j = previousHz; j < hz; j++)
                {
                    //Debug.Log("count: " + count);
                    //Debug.Log(" spectrum: " + audioSpectrum.Length);
                    if (count > Tooling.Base.samples - 1)
                    {
                        break;
                    }
                    average += Tooling.Base.audioSpectrum[count] * ((count + 1) / 2);
                    count++;
                }
                previousHz = hz;

                if (count != 0)
                {
                    average /= count;
                }



                if (average <= 0.001f)
                {
                    average = 0;
                }
                float final = average * 15;

                //Debug.Log("Max bands : "  + bands + " Band " + i + " with value: " + final + " with average of : " + average + " count: " + count + " Spectrum length: " + audioSpectrum.Length);
                Tooling.Base._freqBand[i] = final;
            }

        }
    }
}
