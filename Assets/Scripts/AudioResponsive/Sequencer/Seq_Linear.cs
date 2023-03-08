using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Seq_Linear : MonoBehaviour, I_Sequencable
{
    [SerializeField] private bool isInnate = false;

    public void Start()
    {
        if (isInnate)
        {
            Tooling.Base.onSub += Trigger;
        }
    }

    public void Trigger()
    {
        Debug.Log("Trigger");
    }

}
