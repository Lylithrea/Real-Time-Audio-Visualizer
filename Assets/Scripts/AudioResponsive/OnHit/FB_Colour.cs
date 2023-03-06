using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_Colour : MonoBehaviour
{
    [Tooltip("Min is inclusive, max is exclusive")]
    public int minBand, maxBand;
    [GradientUsage(true)]
    public Gradient test;
    public string property = "colour";
    public float intensity = 1;
    public bool isExponential = false;

    private Material mat;
    private void Start()
    {
        mat = this.gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float value = 0;
        for (int i = minBand; i < maxBand; i++)
        {
            value += Tooling.Base._freqBand[i];
        }
        value /= (maxBand - minBand);
        if (isExponential)
        {
            value = Mathf.Pow(value, 0.25f);
        }
        value *= intensity;
        if (float.IsNaN(value))
        {
            value = 0;
        }

        mat.SetColor("_" + property, test.Evaluate(Mathf.Clamp01(value)));
    }
}
