using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Tooling
{

    public class FrequencyBander
    {

        private static float previousHz = 0;


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
                    average += Tooling.Base.audioSpectrum[count] * ((count + 1) / 2);
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
                float final = average * 15;

                Tooling.Base._freqBand[i] = final;
            }

        }
    }
}
