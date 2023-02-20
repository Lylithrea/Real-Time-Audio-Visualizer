using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooling
{

    public class FrequencyBander
    {

        private static float previousHz = 0;



        // start - start frequency
        // stop - stop frequency
        // n - the point which you wish to compute (zero based)
        // N - the number of points over which to divide the frequency
        // range.
        static float logspace(float start, float stop, int n, int N)
        {
            return start * Mathf.Pow(stop / start, n / (float)(N - 1));
        }

        public static void FrequencyBands()
        {
            int count = 0;

            float normalizer = Mathf.Pow(2, Tooling.Base.bands - 1);
            float steps = Tooling.Base.samples / Tooling.Base.bands;
            int previousTest = 0;

            float previousHzTest = 0;

            for (int i = 0; i < Tooling.Base.bands - 1; i++)
            {
                float average = 0;

                float sampleCount = Mathf.Pow(2, i);
                float normalizedCount = sampleCount / normalizer;
                float hz = normalizedCount * Tooling.Base.samples;


                //float something = 2047 * Mathf.Log(i) / Mathf.Log(63);
                float something = logspace(1, 2047, i, 64);
                Debug.Log(something);
                float test2 = 0;

                test2 = something;
                float blep = 0;
                float blepCount = 0;

                for (float test = previousHzTest; test < something; test++)
                {
                    blep += Tooling.Base.audioSpectrum[Mathf.FloorToInt(test)];
                    blepCount++;
                }
                previousHzTest = something;
                blep /= blepCount;
                test2 = blep;

                #region test

                float testFinal = 0;
                


                for(int s = previousTest; s < Mathf.FloorToInt(i * steps); s++)
                {
                    //Debug.Log(s);
                    testFinal += Tooling.Base.audioSpectrum[s];
                    
                }
                testFinal /= steps;
                previousTest = Mathf.FloorToInt( i * steps);




                //go through all the samples, and add them together
                for (float j = previousHz; j < hz; j++)
                {
                    //Debug.Log("count: " + count);
                    //Debug.Log(" spectrum: " + audioSpectrum.Length);
                    //Debug.Log("previous Hz: " + previousHz + " Current Hz: " + hz);
/*                    if (count > Tooling.Base.samples - 1)
                    {
                        Debug.Log("Bigger than samples");
                        break;
                    }*/
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

                #endregion

                //Debug.Log("Max bands : "  + bands + " Band " + i + " with value: " + final + " with average of : " + average + " count: " + count + " Spectrum length: " + audioSpectrum.Length);
                //Tooling.Base._freqBand[i] = final;

                Tooling.Base._freqBand[i] = test2 * 100;
            }

        }
    }
}
