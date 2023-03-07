using System.Collections;
using System.Collections.Generic;
using Tooling;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class UI_BeatBias : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [Range(0, 9)]
    [SerializeField] private int minPass;
    [Range(0, 9)]
    [SerializeField] private int maxPass;

    private List<float> lowBeatsVol = new List<float>();
    private float averageBeatVol = 0;
    private float bias;
    private float biasTimer = 0;

    // Update is called once per frame
    void Update()
    {
        //how often the bias updates in seconds
        if (biasTimer > 0.15f)
        {
            biasTimer = 0;
            CheckBias();
        }

        biasTimer += Time.deltaTime;



    }

    public void CheckBias()
    {
        //Add the low fequency to the array
        float value = 0;
        for (int i = minPass; i <= maxPass; i++)
        {
            value += Tooling.PitchCalculator.getPitch(3 + i);
        }
        value /= (maxPass - minPass);
        lowBeatsVol.Insert(0, value);
        averageBeatVol = 0;
        //if the array list is longer than the requested amount, it will delete the outdated data
        if (lowBeatsVol.Count > 75)
        {
            lowBeatsVol.RemoveRange(75, lowBeatsVol.Count - 76);
        }
        //Calculate the average volume of all the stored low frequency data
        for (int i = 0; i < lowBeatsVol.Count; i++)
        {
            if (!float.IsNaN(lowBeatsVol[i]))
            {
                averageBeatVol += lowBeatsVol[i];
            }

        }

        averageBeatVol /= lowBeatsVol.Count;

        if (!float.IsNaN(averageBeatVol))
        {
            slider.value = averageBeatVol;
        }


    }

}
