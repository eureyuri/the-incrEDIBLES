using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInstantiate : MonoBehaviour
{
    public AudioSource audioSourceFinishCooking;

    public void PlayAudio() {
        audioSourceFinishCooking.Play();
    }
}
