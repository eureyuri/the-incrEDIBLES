using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnInDish : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TurnInDish: collided ");
        GameObject dish = collision.gameObject;

        if (dish.tag != "Plate") {
            Debug.Log("TurnInDish: not plate");
            UpdateScore(Score.decrementVal);
            Destroy(dish);
            return;
        }

        string[] ingredients = GetIngredientsOnDish(dish);
        int points = Recipes.CompleteAndReplace(ingredients);
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
        int childCount = dish.transform.childCount;
        string[] ingredients = new string[childCount];

        for (int i = 0; i < childCount; i++) {
            GameObject child = dish.transform.GetChild(i).gameObject;
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
