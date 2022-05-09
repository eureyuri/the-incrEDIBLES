using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class trashFood : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public AudioSource audioSourceTrash;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        audioSourceTrash.Play();
    }
}
