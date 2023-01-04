using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioSampler : AudioSampleTooling
{

    public LoopbackAudio loopbackAudio;
    public Assets.Scripts.Audio.AudioVisualizationStrategy strategy;
    public bool isUsingMicrophoneInput = false;
    private Lasp.SpectrumAnalyzer analyzer;


    // Start is called before the first frame update
    public override void Start()
    {
        audioSpectrum = new float[samples];
        _freqBand = new float[bands];
        _gainBand = new float[samples];
        analyzer = GetComponent<Lasp.SpectrumAnalyzer>();

    }

    private void OnValidate()
    {
        _freqBand = new float[bands];
        _gainBand = new float[samples];
    }

    // Update is called once per frame
    public override void Update()
    {
        //AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);
        if (isUsingMicrophoneInput)
        {
            audioSpectrum = analyzer.spectrumArray.ToArray();
        }
        else
        {
            audioSpectrum = loopbackAudio.GetAllSpectrumData(strategy);
        }





        if (audioSpectrum != null && audioSpectrum.Length > 0)
        {
            spectrumValue = audioSpectrum[0] * 100;
        }


        base.Update();
    }
}

