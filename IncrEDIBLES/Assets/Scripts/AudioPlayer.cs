using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSourceNormal;
    public AudioSource audioSourceLast;

    void Start()
    {
        audioSourceNormal.PlayScheduled(AudioSettings.dspTime);

        // Queue next music
        double clipLength = audioSourceNormal.clip.samples / audioSourceNormal.clip.frequency;
        audioSourceLast.PlayScheduled(AudioSettings.dspTime + clipLength);
    }

    // Update is called once per frame
    void Update()
    {
        // Stop audio after 3 min
        if (AudioSettings.dspTime == 180) audioSourceLast.Stop();

    }
}
