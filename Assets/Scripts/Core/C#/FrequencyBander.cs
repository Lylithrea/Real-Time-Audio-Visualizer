using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            //return start * Mathf.Pow(stop / start, n / (float)(N - 1));
            return Mathf.Pow(stop / start, n / (float)(N - 1));
        }

        public static void test()
        {
            int count = 0;

            float normalizer = Mathf.Pow(2, Tooling.Base.bands - 1);
            float steps = Tooling.Base.samples / Tooling.Base.bands;

            for (int i = 0; i < Tooling.Base.bands - 1; i++)
            {
                float average = 0;

                float sampleCount = Mathf.Pow(2, i - 0.75f * i);
                float normalizedCount = sampleCount / normalizer;
                float hz = normalizedCount * Tooling.Base.samples;

                hz = Mathf.Pow(i, 2);

                float value = i + 1;
                hz = 1 / (Mathf.Pow(value, -((19/12) + 17 / (value + 4))));

                hz = Mathf.Pow(2, (value / 6.4f)) * 2;
                //hz = Mathf.Pow(2, (value / 5.82f));
                //hz = Mathf.Pow(2, (value / 16));
                //hz *= 128;


                //hz = -36.90922f + 102.2587f * i - 3.289149f * Mathf.Pow(i, 2) + 0.03438939f * Mathf.Pow(i, 3);
                //hz = -23.11415f - 3.161301f * i + 1.760747f * Mathf.Pow(i, 2) - 0.01886937f * Mathf.Pow(i, 3);

                //go through all the samples, and add them together
                for (float j = previousHz; j < hz; j++)
                {
                    if (count > Tooling.Base.samples - 1)
                    {
                        Debug.Log("Bigger than samples");
                        break;
                    }
                    //average += Tooling.Base.audioSpectrum[count] * ((count + 1) / 2);
                    average += Tooling.Base.audioSpectrum[count] * ((count + 1) / 2);
                    count++;
                }
                Debug.Log("Previous Hz: " + previousHz + " Current Hz: " + hz + " with count: " + count);
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

                Tooling.Base._freqBand[i] = final;
            }
        }

        public static void FrequencyBands()
        {
            test();
            return;

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
                    if (count > Tooling.Base.samples - 1)
                    {
                        Debug.Log("Bigger than samples");
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

                #endregion

                //Debug.Log("Max bands : "  + bands + " Band " + i + " with value: " + final + " with average of : " + average + " count: " + count + " Spectrum length: " + audioSpectrum.Length);
                
                Tooling.Base._freqBand[i] = final;

                //Tooling.Base._freqBand[i] = test2 * 100;
            }

        }
    }
}
