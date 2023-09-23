using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public void PlayJump()
    {
        GetComponent<Animator>().SetTrigger("jump");
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void Reset()
    {
        GetComponent<Animator>().SetTrigger("reset");
    }
}
