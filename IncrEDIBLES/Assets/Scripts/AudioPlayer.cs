using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSourceNormal;
    public AudioSource audioSourceLast;
    public AudioSource audioGameOver;
    private bool isOverPlayed;

    void Start()
    {
        audioSourceNormal.Play();
        isOverPlayed = false;
    }

    void Update()
    {
        // Stop audio after 3 min
        if (Timer.getRemainingTime() <= 0 && !isOverPlayed) {
            audioGameOver.Play();
            isOverPlayed = true;
            audioSourceLast.Stop();
        }

        if (Timer.getRemainingTime() < 35 && Timer.getRemainingTime() > 0 && !audioSourceLast.isPlaying) {
            audioSourceNormal.Stop();
            audioSourceLast.Play();
        }
    }
}
