using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tooling
{

    public class Base 
    {

        public delegate void OnBeat();
        public static OnBeat onBeat;


        public delegate void OnSub();
        public static OnSub onSub;

        public delegate void OnBass();
        public static OnBass onBass;

        public delegate void OnLowMid();
        public static OnLowMid onLowMid;

        public static float[] audioSpectrum;
        public static float[] _freqBand;
        public static float bpm = 60;
        public static float bps = 60;

        public static int bands = 64;
        public static int samples = 2048;
        public static float bias = 0;



    }
}
