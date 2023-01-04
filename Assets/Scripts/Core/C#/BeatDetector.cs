using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooling
{

    public class BeatDetector
    {
        public void OnApplicationQuit()
        {
            Tooling.Base.onBeat = null;

        }

        private static float averageVol = 5;
        private static List<float> lowBeatsVol = new List<float>();
        private static float m_timer;
        private static float biasTimer = 0;
        private static float bpmTimer = 0;

        //TODO: Create dynamic bias, based on total volume of low frequency
        //based on this total volume, adjust bias accordingly
        //implement minimum check timer (instead of every beat)
        //so that if its silent the bpm will be updated.

        public void Beat()
        {
            m_timer = 0;
            BPMCounter();
            Base.onBeat();
        }


        public static void CheckBias()
        {
            lowBeatsVol.Insert(0, PitchCalculator.getLowPitch());
            if (lowBeatsVol.Count > 75)
            {
                for (int i = 0; i < 75; i++)
                {
                    averageVol += lowBeatsVol[i];
                }
                averageVol /= 75;
            }
            else
            {
                for (int i = 0; i < lowBeatsVol.Count; i++)
                {
                    averageVol += lowBeatsVol[i];
                }
                averageVol /= lowBeatsVol.Count;
            }
            Base.bias = averageVol;
        }

        public static void CheckBeat()
        {
            if (biasTimer > 0.25f)
            {
                biasTimer = 0;
                CheckBias();
            }
            if (PitchCalculator.getLowPitch() > averageVol)
            {
                lowBeatsVol.Insert(0, (PitchCalculator.getLowPitch() / 10) * 9.75f);
                if (m_timer > 0.15f)
                    Base.onBeat();
            }

            m_timer += Time.deltaTime;
            biasTimer += Time.deltaTime;
        }

        public void BPMCounter()
        {
            Base.bpm = 60 / bpmTimer;
            bpmTimer = 0;
        }

    }
}
