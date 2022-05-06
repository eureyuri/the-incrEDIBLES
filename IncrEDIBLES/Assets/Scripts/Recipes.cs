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

    private readonly float RECIPE_POS_0 = -25f;
    private readonly float RECIPE_POS_1 = -13.99f;
    private readonly float RECIPE_POS_2 = -3.1f;
    private readonly float RECIPE_POS_3 = 7.16f;
    private readonly float RECIPE_POS_4 = 18.5f;

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
        int ingredientsLen = recipe.ingredients.Length;

        GameObject recipeCard = GetRecipeCard(ingredientsLen);
        if (recipeCard == null) return;

        float xPos = GetRecipePos(index);

        Sprite dish = GetDishImg(recipe.name);
        recipeCard.transform.GetChild(0).GetComponent<Image>().sprite = dish;

        Transform ingredientList = recipeCard.transform.GetChild(1);
        Transform ingredientInst1 = recipeCard.transform.GetChild(2);
        Transform ingredientInst2 = recipeCard.transform.GetChild(3);
        Sprite ingredientToAdd;
        for (int i = 0; i < ingredientsLen; i++) {
            string ingredient = recipe.ingredients[i];
            ingredientToAdd = GetIngredientImg(ingredient);
            ingredientList.GetChild(i).GetComponent<Image>().sprite = ingredientToAdd;

            string[] procedure = GetProcedures(ingredient);

            for (int j = 0; j < procedure.Length; j++) {
                Sprite instToAdd = GetProcedureImg(procedure[j]);

                if (j == 0) {
                    ingredientInst1.GetChild(i).GetComponent<Image>().sprite = instToAdd;
                } else if (j == 1) {
                    ingredientInst2.GetChild(i).GetComponent<Image>().sprite = instToAdd;
                }

            }
        }

        GameObject card = Instantiate(recipeCard, new Vector3(xPos, 25.1f, 0), Quaternion.identity) as GameObject;
        card.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    private GameObject GetRecipeCard(int ingredientsLen) {
        switch (ingredientsLen) {
            case 2: return recipeCard2;
            case 3: return recipeCard3;
            case 4: return recipeCard4;
            default: return null;
        }
    }

    private float GetRecipePos(int index) {
        switch (index) {
            case 0: return RECIPE_POS_0;
            case 1: return RECIPE_POS_1;
            case 2: return RECIPE_POS_2;
            case 3: return RECIPE_POS_3;
            case 4: return RECIPE_POS_4;
            default: return 0;
        }
    }

    private Sprite GetDishImg(string name) {
        switch (name) {
            case "tomato pasta": return tomatoPasta;
            case "tomato cheese pasta": return tomatoCheesePasta;
            case "tomato mushroom pasta": return tomatoMushroomPasta;
            case "tomato meat pasta": return tomatoMeatPasta;
            case "tomato fish pasta": return tomatoFishPasta;
            case "tomato mushroom fish pasta": return tomatoMushroomFishPasta;
            case "tomato mushroom meat pasta": return tomatoMushroomMeatPasta;
            case "cheese pasta": return cheesePasta;
            case "cheese mushroom pasta": return cheeseMushroomPasta;
            case "cheese meat pasta": return cheeseMeatPasta;
            case "cheese fish pasta": return cheeseFishPasta;
            case "cheese mushroom fish pasta": return cheeseMushroomFishPasta;
            case "cheese mushroom meat pasta": return cheeseMushroomMeatPasta;
            default: return null;
        }
    }

    private Sprite GetIngredientImg(string ingredient) {
        switch (ingredient) {
            case "pasta": return pasta;
            case "fish": return fish;
            case "mushroom": return mushroom;
            case "tomato": return tomato;
            case "cheese": return cheese;
            case "meat": return meat;
            default: return null;
        }
    }

    private string[] GetProcedures(string ingredient) {
        for (int i = 0; i < recipes.directions.Length; i++) {
            if (ingredient == recipes.directions[i].name) {
                return recipes.directions[i].procedure;
            }
        }
        return null;
    }

    private Sprite GetProcedureImg(string procedure) {
        switch (procedure) {
            case "boil": return boil;
            case "cut": return cut;
            case "saute": return saute;
            default: return null;
        }
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
