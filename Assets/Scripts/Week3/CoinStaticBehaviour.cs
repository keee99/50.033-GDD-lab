using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStaticBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.IncreaseScore(1);
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().Play("CoinStaticCollect");
        }
    }

    public void PlayCoinSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

}
