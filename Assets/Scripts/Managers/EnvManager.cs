using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : ResetterBaseClass
{
    public override void Reset()
    {
        base.Reset();
        ResetMixer();
    }

    public void ResetMixer()
    {
        // Unimplemented
        // AudioMixerManager.Instance.Reset();
    }
}