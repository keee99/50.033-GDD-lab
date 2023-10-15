using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnvManager : MonoBehaviour
{
    public void Reset()
    {
        // base.Reset();
        ResetMixer();
    }

    public void ResetMixer()
    {
        AudioSource audio = GetComponent<AudioSource>();
        // Reset mixer of current audio
        AudioMixer mixer = audio.outputAudioMixerGroup.audioMixer;
        // Switch to default snapshot
        mixer.FindSnapshot("Default").TransitionTo(0f);
        audio.Play();
    }
}