using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class trashFood : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    void OnCollisionEnter(Collision collision)
    {
        score = int.Parse(scoreText.text);
        Destroy(collision.gameObject);
        if (collision.gameObject.CompareTag("OverCooked"))
        {
            score -= 2;
        }
        else 
        {
            score -= 4;
        }
        if(score<0)
        {
            scoreText.color = new Color(255, 0, 0, 255);
        }
        scoreText.text = score.ToString();
    }
}
