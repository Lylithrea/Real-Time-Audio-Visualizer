using System.Collections;
using System.Collections.Generic;
using Tooling;
using UnityEngine;


public class ARH_Move : MonoBehaviour
{

    [SerializeField]public Position position;
    public float speed = 1;
    public float distance = 10;
    public Tooling.Zones zone;

    private Vector3 originalPosition;
    private bool isMoving = false;
    float time = 0;

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
        originalPosition = this.transform.position;
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
            this.transform.position = originalPosition + new Vector3(distance * position.x.Evaluate(Mathf.Clamp01(time)), distance * position.y.Evaluate(Mathf.Clamp01(time)), distance * position.z.Evaluate(Mathf.Clamp01(time)));

            if (time >= 1)
            {
                isMoving = false;
            }
        }
    }


}

[System.Serializable]
public class Position
{
    public AnimationCurve x = AnimationCurve.Constant(0,0,0);
    public AnimationCurve y = AnimationCurve.Constant(0, 0, 0);
    public AnimationCurve z = AnimationCurve.Constant(0, 0, 0);

}