using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundCallback : MonoBehaviour
{
    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
