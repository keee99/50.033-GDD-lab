using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IInteractiveButton
{

    public AudioMixer mixer;
    private AudioMixerSnapshot softSnapShot;
    private AudioMixerSnapshot defaultSnapShot;

    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;

    private PlayerInput marioActions;

    public Canvas pauseOverlay;


    public UnityEvent onGamePause;
    public UnityEvent onGameResume;

    void Start()
    {
        image = GetComponent<Image>();
        marioActions = GameObject.Find("Mario").GetComponent<PlayerInput>();
        defaultSnapShot = mixer.FindSnapshot("Default");
        softSnapShot = mixer.FindSnapshot("Soft");
    }


    public void ButtonClick()
    {
        PlaySound();
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;
        if (isPaused)
        {
            // marioActions.actions.Disable();
            onGamePause.Invoke();
            togglePauseUI(true);
            softSnapShot.TransitionTo(0f);
        }
        else
        {
            togglePauseUI(false);
            // marioActions.actions.Enable();
            onGameResume.Invoke();
            defaultSnapShot.TransitionTo(0f);

        }

    }

    public void togglePauseUI(bool toggle)
    {
        pauseOverlay.gameObject.SetActive(toggle);
        if (toggle)
        {
            image.sprite = playIcon;
        }
        else
        {
            image.sprite = pauseIcon;
        }

    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
