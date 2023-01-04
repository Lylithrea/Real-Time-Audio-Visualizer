using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tooling;

public class AudioSampleTooling : MonoBehaviour
{
    public delegate void OnBeat();
    public static OnBeat onBeat;


    public static float spectrumValue;
    public static float avgSpectrumValue;

    protected float lowPitch;
    protected float midPitch;
    protected float highPitch;



    protected float bpmTimer = 0;


    public void OnApplicationQuit()
    {
        onBeat = null;
    }


    public virtual void Start()
    {
        onBeat();
    }

    public virtual void Update()
    {
        bpmTimer += Time.deltaTime;
        Tooling.FrequencyBander.FrequencyBands();
        Tooling.BeatDetector.CheckBeat();
    }



}
