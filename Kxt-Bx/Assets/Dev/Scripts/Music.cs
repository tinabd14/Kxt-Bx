using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip thrustAudio;
    [SerializeField] private AudioClip successAudio;
    [SerializeField] private AudioClip deathAudio;

    [SerializeField] private Rocket rocket;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayAppropriateMusic();
    }

    private void PlayAppropriateMusic()
    {
        if(GameManager.gState == GameManager.gameState.playing)
        {
            if(rocket.GetIsFlying())
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(thrustAudio);
                }
            }
            else
            {
                audioSource.Stop();
            }
        }
        else if(GameManager.gState == GameManager.gameState.success)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(successAudio);
            }
        }
        else if(GameManager.gState == GameManager.gameState.end)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(deathAudio);
            }
        }
    }
}
