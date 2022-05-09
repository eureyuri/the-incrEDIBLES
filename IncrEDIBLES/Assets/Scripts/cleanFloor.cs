using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cleanFloor : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    public AudioSource audioSourceWrong;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        audioSourceWrong.Play();

        Score.adjust(-5);
        if (Score.score < 0)
        {
            scoreText.color = new Color(255, 0, 0, 255);
        }
        scoreText.text = Score.score.ToString();
    }
}
