using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tooling
{
    public class Base 
    {

        public delegate void OnBeat();
        public static OnBeat onBeat;


        public static float[] audioSpectrum;
        public static float[] _freqBand;
        public static float bpm;

        public static int bands = 64;
        public static int samples = 2048;
        public static float bias = 50;



    }
}
