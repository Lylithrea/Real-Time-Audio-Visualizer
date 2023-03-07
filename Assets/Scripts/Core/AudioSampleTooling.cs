using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tooling;

public class AudioSampleTooling : MonoBehaviour
{



    public static float spectrumValue;
    public static float avgSpectrumValue;

    protected float lowPitch;
    protected float midPitch;
    protected float highPitch;



    protected float bpmTimer = 0;


    public void OnApplicationQuit()
    {
        Tooling.Base.onBeat = null;
        Tooling.Base.onSub = null;
        Tooling.Base.onBass = null;
        Tooling.Base.onLowMid = null;

    }



    public virtual void Update()
    {
        bpmTimer += Time.deltaTime;
        Tooling.FrequencyBander.FrequencyBands();
        Tooling.HitDetector.CheckHits();
        Tooling.HiHatDetector.CheckHiHatHit();
    }



}
