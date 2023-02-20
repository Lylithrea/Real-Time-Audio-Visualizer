using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_Size : MonoBehaviour
{
    [Tooltip("Min is inclusive, max is exclusive")]
    public int minBand, maxBand;
    public float sensitivity = 1;

    private Vector3 originalScale = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
          originalScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float intensity = 0;
        for (int i = minBand; i < maxBand; i++)
        {
            intensity += Tooling.Base._freqBand[i];
        }
        intensity /= (maxBand - minBand);
        intensity *= sensitivity;
        if (float.IsNaN(intensity))
        {
            intensity = 0;
        }

        this.gameObject.transform.localScale = originalScale + new Vector3(0,intensity, 0);
    }





}
