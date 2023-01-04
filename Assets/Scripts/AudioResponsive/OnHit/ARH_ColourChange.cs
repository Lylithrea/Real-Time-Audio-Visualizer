using System.Collections;
using System.Collections.Generic;
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


    // Start is called before the first frame update
    void Start()
    {
        Tooling.Base.onBeat += onBeat;
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
