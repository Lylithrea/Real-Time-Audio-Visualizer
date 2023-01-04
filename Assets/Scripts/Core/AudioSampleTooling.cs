using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class AudioSampleTooling : MonoBehaviour
    {
        public delegate void OnBeat();
        public static OnBeat onBeat;


        public static float spectrumValue;
        public static float avgSpectrumValue;
        public static float[] _freqBand;
        public static float[] _gainBand;
        public static float bpm;

        public static int bands = 64;

        public int samples = 2048;
        public float maxVolume = 50;
        public float bias = 50;

        protected float lowPitch;
        protected float midPitch;
        protected float highPitch;

        protected float m_timer;
        protected float[] audioSpectrum;
        protected float bpmTimer = 0;

        protected float biasTimer = 0;


        public delegate void toggleObjects();
        public static toggleObjects onToggle;

        public delegate void resetObjects();
        public static resetObjects onResetObjects;


        public void Awake()
        {
            onBeat = null;
            onToggle = null;
            onResetObjects = null;
        }

        public void OnApplicationQuit()
        {
            onBeat = null;
            onToggle = null;
            onResetObjects = null;
        }


        public virtual void Start()
        {
            onBeat();
        }

        public virtual void Update()
        {
            bpmTimer += Time.deltaTime;
            FrequencyBands();
            CheckBeat();
            getDynamicVolume();
        }

        public float volTimer = 0;
        private float avgVol = 0;
        private List<float> volumes = new List<float>();








        public void getDynamicVolume()
        {
            if (volTimer > 1)
            {
                volumes.Insert(0, PitchCalculator.getLowPitch());
                volumes.Insert(0, PitchCalculator.getMidPitch());
                volumes.Insert(0, PitchCalculator.getHighPitch());

                if (volumes.Count > 500)
                {
                    for (int i = 0; i < 500; i++)
                    {
                        avgVol += volumes[i];
                    }
                    avgVol /= 500;
                }
                else
                {
                    for (int i = 0; i < volumes.Count; i++)
                    {
                        avgVol += volumes[i];
                    }
                    avgVol /= volumes.Count;
                }
                avgSpectrumValue = avgVol;

                volTimer = 0;
            }

            volTimer += Time.deltaTime;
        }













        private float averageVol = 5;
        private List<float> lowBeatsVol = new List<float>();

        //TODO: Create dynamic bias, based on total volume of low frequency
        //based on this total volume, adjust bias accordingly
        //implement minimum check timer (instead of every beat)
        //so that if its silent the bpm will be updated.

        public void Beat()
        {
            m_timer = 0;
            BPMCounter();
            onBeat();
        }


        public void CheckBias()
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
            bias = averageVol;
        }

        public void CheckBeat()
        {
            if (biasTimer > 0.25f)
            {
                biasTimer = 0;
                CheckBias();
            }

            if (PitchCalculator.getLowPitch() > averageVol)
            {
                lowBeatsVol.Insert(0, (PitchCalculator.getLowPitch() /  10) * 9.75f );
                if (m_timer > 0.15f)
                    Beat();
            }

            m_timer += Time.deltaTime;
            biasTimer += Time.deltaTime;
        }

        public void BPMCounter()
        {
            bpm = 60 / bpmTimer;
            bpmTimer = 0;
        }









    



    public static float getVolume()
        {
            float value = 0;
            for (int i = 0; i < _freqBand.Length; i++)
            {
                value += _freqBand[i];
            }
            value /= _freqBand.Length;
            return value;
        }


        private float previousHz = 0;
        public void FrequencyBands()
        {
            int count = 0;

            float normalizer = Mathf.Pow(2, bands - 1);

            for (int i = 0; i < bands - 1; i++)
            {
                //Debug.Log(i);
                float average = 0;

                float sampleCount = Mathf.Pow(2, i);
                float normalizedCount = sampleCount / normalizer;
                float hz = normalizedCount * samples;

                //go through all the samples, and add them together
                for (float j = previousHz; j < hz; j++)
                {
                    //Debug.Log("count: " + count);
                    //Debug.Log(" spectrum: " + audioSpectrum.Length);
                    if (count > samples - 1)
                    {
                        break;
                    }
                    average += audioSpectrum[count] * ((count + 1) / 2);
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
                _freqBand[i] = final;
            }

        }

    }
