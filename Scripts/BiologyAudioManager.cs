using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiologyAudioManager : MonoBehaviour
{
    /*
     * Implemented an Audio Manager in order to avoid overlapping audio and
     * just pass all audio clips into one audio player
     * 
     */

    public AudioClip currentAudio;
    public AudioSource currentAudioSource;

    public void getAudioInfoToPlay(AudioClip clip)
    {
        currentAudio = clip;
        currentAudioSource.clip = currentAudio;
        playCurrentAudio();
    }

    private void playCurrentAudio()
    {
        if (currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
            currentAudioSource.Play();
        }
        else
            currentAudioSource.Play();
    }
}