using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Seq_Random : MonoBehaviour, I_Sequencable
{

    [SerializeField] private bool isInnate = false;


    [SerializeField, InterfaceType(typeof(I_Sequencable))]
    public Object[] myObjects;
    public List<I_Sequencable> effects => myObjects.OfType<I_Sequencable>().ToList();


    [field: SerializeField] public bool playFullSequence { get; set; }

    [field: SerializeField] public bool doNotReset { get; set; }

    public bool isPlaying { get; set; }

    public float timer;
    private float currentTimer = 0;
    public bool isLoop = false;
    public int currentEffect = 0;
    public bool isTrulyRandom = false;
    public int amount = 5;
    public int currentAmount = 0;


    private List<I_Sequencable> availableEffects = new List<I_Sequencable>();


    // Start is called before the first frame update
    void Start()
    {
        availableEffects.AddRange(effects);
        currentEffect = Random.Range(0, effects.Count);
        if (isInnate)
        {
            Tooling.Base.onSub += DoTrigger;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (currentTimer <= 0)
            {
                currentTimer = timer;

                if (isTrulyRandom)
                {
                    if (effects[currentEffect].Trigger())
                    {
                        currentEffect = Random.Range(0, effects.Count);
                    }
                }

                else if (availableEffects[currentEffect].Trigger())
                {
                    availableEffects.RemoveAt(currentEffect);
                    currentEffect = Random.Range(0, availableEffects.Count);

                    if (availableEffects.Count == 0)
                    {
                        availableEffects.AddRange(effects);
                        currentEffect = Random.Range(0, availableEffects.Count);

                        if (!isLoop)
                        {
                            isPlaying = false;
                            return;
                        }
                    }
                }


            }
            else
            {
                currentTimer -= Time.deltaTime;
            }

        }
    }

    public void DoTrigger()
    {
        Trigger();
    }

    public bool Trigger()
    {

        if (currentAmount >= amount)
        {
            currentAmount = 0;
            return true;
        }

        if (playFullSequence)
        {
            isPlaying = true;
            return true;
        }


        //check if the last effect was always playing, if so, then disable it
        if (currentEffect == 0)
        {
            if (effects[effects.Count - 1].playFullSequence)
            {
                resetLastEffect(effects.Count - 1);
            }
        }
        else if (effects[currentEffect - 1].playFullSequence)
        {
            resetLastEffect(currentEffect - 1);
        }

        //check if the next one is done, if so increase the effect count
        if (isTrulyRandom)
        {
            if (effects[currentEffect].Trigger())
            {
                currentEffect = Random.Range(0, effects.Count);
                currentAmount++;
            }

        }
        else
        {
            if (availableEffects[currentEffect].Trigger())
            {
                availableEffects.RemoveAt(currentEffect);
                if (availableEffects.Count == 0)
                {
                    availableEffects.AddRange(effects);
                    currentEffect = Random.Range(0, availableEffects.Count);

                }
                currentEffect = Random.Range(0, availableEffects.Count);
                currentAmount++;
            }
        }

        return false;
    }

    public void ResetEffect()
    {
        availableEffects.Clear();
        availableEffects.AddRange(effects);
        currentEffect = Random.Range(0, effects.Count);
    }



    void resetLastEffect(int effectCount)
    {
        effects[effectCount].isPlaying = false;
        if (!effects[effectCount].doNotReset)
        {
            effects[effectCount].ResetEffect();
        }
    }

}
