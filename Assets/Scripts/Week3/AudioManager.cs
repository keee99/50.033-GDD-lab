using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    private AudioMixerSnapshot snapshot;
    public AudioMixer mixer;

    private readonly string SNAPSHOT_DEEP_FRIED = "DeepFried";
    private readonly string SNAPSHOT_DEFAULT = "Default";

    public void ChangeSnapshotDeepFry()
    {
        // instantiate somewhere in the code
        snapshot = mixer.FindSnapshot(SNAPSHOT_DEEP_FRIED);

        // then transition
        snapshot.TransitionTo(.2f); //transition to snapshot
    }

    public void ChangeSnapshotDefault()
    {
        // instantiate somewhere in the code
        snapshot = mixer.FindSnapshot(SNAPSHOT_DEFAULT);

        // then transition
        snapshot.TransitionTo(.2f); //transition to snapshot
    }
}
