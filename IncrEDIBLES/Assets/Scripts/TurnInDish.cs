using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnInDish : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public AudioSource audioSourceCorrect;
    public AudioSource audioSourceWrong;

    void OnCollisionEnter(Collision collision)
    {
        GameObject dish = collision.gameObject;

        if (dish.tag != "Plate") {
            audioSourceWrong.Play();
            UpdateScore(Score.decrementVal);
            Destroy(dish);
            return;
        }

        CombineFood c = dish.GetComponent<CombineFood>();
        string[] ingredients = c.GetFoodOnPlate().ToArray();
        int points = Recipes.CompleteAndReplace(ingredients);
        if (points < 0) audioSourceWrong.Play();
        else audioSourceCorrect.Play();
        Debug.Log("TurnInDish: points: " + points);
        UpdateScore(points);

        Destroy(dish);
    }

    private void UpdateScore(int val) {
        Score.adjust(val);
        UpdateScoreText();
    }

    private void UpdateScoreText() {
        int score = Score.score;
        if(score < 0)
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
