using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using System;
using System.Linq;

public class Recipes : MonoBehaviour
{
    public TextAsset jsonRecipes;
    public AllRecipe recipes;
    public GameObject recipeCard2;
    public GameObject recipeCard3;
    public GameObject recipeCard4;
    public Sprite tomatoPasta;
    public Sprite tomatoCheesePasta;
    public Sprite tomatoMushroomPasta;
    public Sprite tomatoMeatPasta;
    public Sprite tomatoFishPasta;
    public Sprite tomatoMushroomFishPasta;
    public Sprite tomatoMushroomMeatPasta;
    public Sprite cheesePasta;
    public Sprite cheeseMushroomPasta;
    public Sprite cheeseMeatPasta;
    public Sprite cheeseFishPasta;
    public Sprite cheeseMushroomFishPasta;
    public Sprite cheeseMushroomMeatPasta;
    public Sprite pasta;
    public Sprite fish;
    public Sprite mushroom;
    public Sprite tomato;
    public Sprite cheese;
    public Sprite meat;
    public Sprite cut;
    public Sprite boil;
    public Sprite saute;

    public static RecipeInfo[] currRecipes;
    private readonly int NUM_RECIPES = 5;
    private int allRecipeCount;
    Random rand = new Random();

    [System.Serializable]
    public class RecipeInfo {
        public string name;
        public string[] ingredients;
        public int score;
    }

    [System.Serializable]
    public class Directions {
        public string name;
        public string[] procedure;
    }

    [System.Serializable]
    public class AllRecipe {
        public RecipeInfo[] recipes;
        public Directions[] directions;
    }

    void Start()
    {
        recipes = JsonUtility.FromJson<AllRecipe>(jsonRecipes.text);
        currRecipes = new RecipeInfo[NUM_RECIPES];

        allRecipeCount = recipes.recipes.Length;
        GenerateRecipes();
        InstantiateRecipes();
    }

    private void GenerateRecipes() {
        for (int i = 0; i < NUM_RECIPES; i++) currRecipes[i] = PickRecipe();
    }

    public RecipeInfo PickRecipe() {
        int i = rand.Next(allRecipeCount);
        return recipes.recipes[i];
    }

    private void InstantiateRecipes() {
        for (int i = 0; i < NUM_RECIPES; i++) InstantiateRecipe(currRecipes[i], i);
    }

    public void InstantiateRecipe(RecipeInfo recipe, int index) {
        GameObject recipeCard;
        int ingredientsLen = recipe.ingredients.Length;
        switch (ingredientsLen) {
            case 2: recipeCard = recipeCard2; break;
            case 3: recipeCard = recipeCard3; break;
            case 4: recipeCard = recipeCard4; break;
            default: recipeCard = null; break;
        }

        if (recipeCard == null) return;

        float xPos;
        switch (index) {
            case 0: xPos = -19f; break;
            case 1: xPos = -7.99f; break;
            case 2: xPos = 3.1f; break;
            case 3: xPos = 14.16f; break;
            case 4: xPos = 25.5f; break;
            default: xPos = 0; break;
        }

        Sprite dish;
        switch (recipe.name) {
            case "tomato pasta": dish = tomatoPasta; break;
            case "tomato cheese pasta": dish = tomatoCheesePasta; break;
            case "tomato mushroom pasta": dish = tomatoMushroomPasta; break;
            case "tomato meat pasta": dish = tomatoMeatPasta; break;
            case "tomato fish pasta": dish = tomatoFishPasta; break;
            case "tomato mushroom fish pasta": dish = tomatoMushroomFishPasta; break;
            case "tomato mushroom meat pasta": dish = tomatoMushroomMeatPasta; break;
            case "cheese pasta": dish = cheesePasta; break;
            case "cheese mushroom pasta": dish = cheeseMushroomPasta; break;
            case "cheese meat pasta": dish = cheeseMeatPasta; break;
            case "cheese fish pasta": dish = cheeseFishPasta; break;
            case "cheese mushroom fish pasta": dish = cheeseMushroomFishPasta; break;
            case "cheese mushroom meat pasta": dish = cheeseMushroomMeatPasta; break;
            default: dish = null; break;
        }
        recipeCard.transform.GetChild(0).GetComponent<Image>().sprite = dish;

        Transform ingredientList = recipeCard.transform.GetChild(1);
        Transform ingredientInst1 = recipeCard.transform.GetChild(2);
        Transform ingredientInst2 = recipeCard.transform.GetChild(3);
        Sprite ingredientToAdd;
        for (int i = 0; i < ingredientsLen; i++) {
            switch (recipe.ingredients[i]) {
                case "pasta": ingredientToAdd = pasta; break;
                case "fish": ingredientToAdd = fish; break;
                case "mushroom": ingredientToAdd = mushroom; break;
                case "tomato": ingredientToAdd = tomato; break;
                case "cheese": ingredientToAdd = cheese; break;
                case "meat": ingredientToAdd = meat; break;
                default: ingredientToAdd = null; break;
            }
            ingredientList.GetChild(i).GetComponent<Image>().sprite = ingredientToAdd;

            string[] procedure = null;
            Debug.Log("searching procedure 1: " + recipe.ingredients[i]);
            for (int j = 0; j < recipes.directions.Length; j++) {
                Debug.Log("searching procedure 2: " + recipes.directions[j].name);
                if (recipe.ingredients[i] == recipes.directions[j].name) {
                    procedure = recipes.directions[j].procedure;
                    break;
                }
            }
            Debug.Log("final procedure: " + procedure);
            for (int j = 0; j < procedure.Length; j++) {
                Sprite instToAdd;
                switch (procedure[j]) {
                    case "boil": instToAdd = boil; break;
                    case "cut": instToAdd = cut; break;
                    case "saute": instToAdd = saute; break;
                    default: instToAdd = null; break;
                }

                Debug.Log("appending procedure: " + procedure[j]);
                if (j == 0) {
                    ingredientInst1.GetChild(i).GetComponent<Image>().sprite = instToAdd;
                } else if (j == 1) {
                    ingredientInst2.GetChild(i).GetComponent<Image>().sprite = instToAdd;
                }

            }
            Debug.Log("finished: " + i);
        }

        GameObject card = Instantiate(recipeCard, new Vector3(xPos, 25.1f, 0), Quaternion.identity) as GameObject;
        card.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
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
