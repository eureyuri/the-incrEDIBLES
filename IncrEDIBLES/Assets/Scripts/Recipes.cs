using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System;
using System.Linq;

public class Recipes : MonoBehaviour
{
    public TextAsset jsonRecipes;
    public AllRecipe recipes;
    public static RecipeInfo[] currRecipes;
    private readonly int NUM_RECIPES = 5;
    private int allRecipeCount;
    Random rand = new Random();

    // [System.Serializable]
    // public class Ingredient {
    //     public string food;

    //     public override string ToString() {
    //         return food;
    //     }
    // }

    [System.Serializable]
    public class RecipeInfo {
        public string name;
        public string[] ingredients;
        public int score;
    }

    [System.Serializable]
    public class Directions {
        public string[] directions;
    }

    [System.Serializable]
    public class AllRecipe {
        public RecipeInfo[] recipes;
        public Directions directions;
    }

    void Start()
    {
        recipes = JsonUtility.FromJson<AllRecipe>(jsonRecipes.text);
        currRecipes = new RecipeInfo[NUM_RECIPES];

        allRecipeCount = recipes.recipes.Length;
        GenerateRecipes();

        Debug.Log("currRecipes: " + currRecipes);
    }

    private void GenerateRecipes() {
        for (int i = 0; i < NUM_RECIPES; i++) currRecipes[i] = PickRecipe();
    }

    public RecipeInfo PickRecipe() {
        int i = rand.Next(allRecipeCount);
        return recipes.recipes[i];
    }

    public bool CheckComplete(string[] ingredients) {
        for (int i = 0; i < NUM_RECIPES; i++) {
            // string[] currIngredients = Array.ConvertAll(currRecipes[i].ingredients, x => x.ToString());

            // Check if correct. If correct replace the recipe with a new one
            if (currRecipes[i].ingredients.SequenceEqual(ingredients)) {
                currRecipes[i] = PickRecipe();
                return true;
            }
        }

        return false;
    }

}
