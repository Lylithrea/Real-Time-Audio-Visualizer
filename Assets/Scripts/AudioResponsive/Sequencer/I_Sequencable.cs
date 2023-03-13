using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Sequencable
{
    bool playFullSequence { get; set; }
    bool isPlaying { get; set; }
    bool doNotReset { get; set; }

    bool Trigger();
    void ResetEffect();

}
