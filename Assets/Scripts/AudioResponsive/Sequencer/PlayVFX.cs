using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayVFX : MonoBehaviour, I_Sequencable
{
    public bool playFullSequence { get; set; }
    public bool isPlaying { get ; set ; }
    public bool doNotReset { get; set ; }

    public void ResetEffect()
    {
        //throw new System.NotImplementedException();
    }

    public bool Trigger()
    {
        //throw new System.NotImplementedException();
        GetComponent<VisualEffect>().Play();
        return true;
    }

}
