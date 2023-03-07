using System.Collections;
using System.Collections.Generic;
using Tooling;
using UnityEngine;

public class ARH_ColourChange : MonoBehaviour
{
    [GradientUsage(true)]
    public Gradient test;
    public float speed = 1;

    private bool isMoving = false;
    float time = 0;
    public string property;
    private Material mat;

    public Tooling.Zones zone;

    // Start is called before the first frame update
    void Start()
    {

        switch (zone)
        {
            case Zones.sub:
                Tooling.Base.onSub += onBeat;
                break;
            case Zones.bass:
                Tooling.Base.onBass += onBeat;
                break;
            case Zones.lowMid:
                Tooling.Base.onLowMid += onBeat;
                break;
            case Zones.mid:
                break;
            case Zones.highmid:
                break;
            case Zones.presence:
                break;
            case Zones.brilliance:
                break;
            default:
                Debug.LogWarning("Zone is not supported.");
                break;
        }
        mat = GetComponent<MeshRenderer>().material;
    }

    void onBeat()
    {
        isMoving = true;
        time = 0;
    }

    private void Update()
    {
        if (isMoving)
        {

            time += speed * Time.deltaTime;
            mat.SetColor("_" + property ,test.Evaluate(Mathf.Clamp01(time)));

            if (time >= 1)
            {
                isMoving = false;
            }
        }
    }

}
