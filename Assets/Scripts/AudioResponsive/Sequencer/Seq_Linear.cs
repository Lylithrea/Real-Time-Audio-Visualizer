using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;
using WraithavenGames.UnityInterfaceSupport;
using System.Linq;

public class Seq_Linear : MonoBehaviour, I_Sequencable
{
    [SerializeField] private bool isInnate = false;


    [SerializeField, InterfaceType(typeof(I_Sequencable))]
    public Object[] myObjects;
    public List<I_Sequencable> effects => myObjects.OfType<I_Sequencable>().ToList();


    [field: SerializeField] public bool playFullSequence { get; set; }

    [field: SerializeField] public bool doNotReset { get; set ; }

    public bool isPlaying { get; set; }

    public bool isLoop = false;
    public float timer;
    private float currentTimer = 0;
    public int currentEffect = 0;


    public void Start()
    {
        if (isInnate)
        {
            Tooling.Base.onSub += DoTrigger;
        }
    }



    public void DoTrigger()
    {
        Trigger();
    }



    public void Update()
    {

        if (isPlaying)
        {
            if (currentTimer <= 0)
            {
                currentTimer = timer;

                //we trigger the effect, it will return true the moment it reached the end
                if (effects[currentEffect].Trigger())
                {
                    currentEffect++;
                }
                //check what we do when we reached the end
                if (currentEffect >= effects.Count)
                {
                    //if we loop it, we simply reset the effect count
                    if (isLoop)
                    {
                        currentEffect = 0;
                    }
                    //otherwise we disable this loop by disabling the boolean, and reset the effect to 0
                    else
                    {
                        isPlaying = false;
                        currentEffect = 0;
                    }
                }
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }
    }



    //true means its done
    public  bool Trigger()
    {
        //if we play the full sequence, we immediatly return true as its done.
        //put boolean to true so update loop can start
        if (playFullSequence)
        {
            isPlaying = true;
            return true;
        }

        //check if the last effect was always playing, if so, then disable it
        //if we are at the start of the list, check the end of the list, as this is a linear sequencer
        if (currentEffect == 0)
        {
            if (effects[effects.Count - 1].playFullSequence)
            {
                resetLastEffect(effects.Count - 1);
            }
        }
        //else we just check the last effect
        else if (effects[currentEffect - 1].playFullSequence)
        {
            resetLastEffect(currentEffect - 1);
        }


        //check if the next one is done, if so increase the effect count
        if (effects[currentEffect].Trigger())
        {
            currentEffect++;
        }

        //check if we reached the end
        if (currentEffect == effects.Count)
        {
            currentEffect = 0;
            return true;
        }

        return false;

    }

    //reset this effect to 0.
    public void ResetEffect()
    {
        currentEffect = 0;
    }

    //when going to next effect, reset last effect if necessary
    void resetLastEffect(int effectCount)
    {
        effects[effectCount].isPlaying = false;
        //reset only if the last effect is always playing, and if it do not reset is false
        if (!effects[effectCount].doNotReset && effects[effectCount].playFullSequence)
        {
            effects[effectCount].ResetEffect();
        }

    }

}
