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

        string[] ingredients = GetIngredientsOnDish(dish);
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


    private string[] GetIngredientsOnDish(GameObject dish) {
        Transform pasta = dish.transform.GetChild(0);

        // pasta is the parent of the other ingredients so need to +1 for pasta
        int childCount = pasta.childCount + 1;
        string[] ingredients = new string[childCount];

        ingredients[0] = GetIngredientFromTag(pasta.gameObject.tag);

        for (int i = 1; i < childCount; i++) {
            GameObject child = pasta.GetChild(i - 1).gameObject;
            ingredients[i] = GetIngredientFromTag(child.tag);
        }

        return ingredients;
    }

    private string GetIngredientFromTag(string tag) {
        switch(tag) {
            case "ReadyPasta": return "pasta";
            case "ReadyTomato": return "tomato";
            case "ReadyCheese": return "cheese";
            case "ReadyMeat": return "meat";
            case "ReadyMushroom": return "mushroom";
            case "ReadyFish": return "fish";
            default: return "";
        }
    }
}
