using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tooling;



public class AudioSampler : AudioSampleTooling
{

    public LoopbackAudio loopbackAudio;
    public Assets.Scripts.Audio.AudioVisualizationStrategy strategy;
    public bool isUsingMicrophoneInput = false;
    private Lasp.SpectrumAnalyzer analyzer;


    // Start is called before the first frame update
    public override void Start()
    {
        Base.audioSpectrum = new float[Tooling.Base.samples];
        Base._freqBand = new float[Tooling.Base.bands];
        analyzer = GetComponent<Lasp.SpectrumAnalyzer>();

    }

    private void OnValidate()
    {
        Base._freqBand = new float[Tooling.Base.bands];
    }

    // Update is called once per frame
    public override void Update()
    {
        //AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);
        if (isUsingMicrophoneInput)
        {
            //lasp
            Base.audioSpectrum = analyzer.spectrumArray.ToArray();
        }
        else
        {
            //WASAPI
            Base.audioSpectrum = loopbackAudio.GetAllSpectrumData(strategy);
            //AudioListener.GetSpectrumData(Base.audioSpectrum, 0, FFTWindow.Hamming);
        }





        if (Base.audioSpectrum != null && Base.audioSpectrum.Length > 0)
        {
            spectrumValue = Base.audioSpectrum[0] * 100;
        }


        base.Update();
    }
}

