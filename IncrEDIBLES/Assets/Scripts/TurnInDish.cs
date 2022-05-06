using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnInDish : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    void OnCollisionEnter(Collision collision)
    {
        score = int.Parse(scoreText.text);
        Destroy(collision.gameObject);
        score += 10;
        Score.score += 10;
        /*if (collision.gameObject.CompareTag("OverCooked"))
        {
            score -= 1;
        }
        else 
        {
            score += 10;
        }*/
        if(score<0)
        {
            scoreText.color = new Color(255, 0, 0, 255);
        }
        else
        {
            scoreText.color = new Color(255, 255, 255, 255);
        }
        scoreText.text = score.ToString();
    }
}
