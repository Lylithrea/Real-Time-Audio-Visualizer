using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARH_Move : MonoBehaviour
{
    public AnimationCurve test;
    public float speed = 1;
    public float distance = 10;

    private Vector3 originalPosition;
    private bool isMoving = false;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioSampler.onBeat += onBeat;
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
            this.transform.position = originalPosition + new Vector3(distance * test.Evaluate(Mathf.Clamp01(time)), 0, 0);

            if (time >= 1)
            {
                isMoving = false;
            }
        }
    }

}
