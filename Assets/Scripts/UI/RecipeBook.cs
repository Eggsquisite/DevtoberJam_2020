using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour {


    public List<Potion> potions;
    private int pageIndex;

    private Image[] recipeIcons;
    private TextMeshProUGUI[] recipeIngredientText;
    private TextMeshProUGUI[] ingredientQuantityText;
    private Image brewItButton;
    private Color brewItButtonDisabledColor = new Color(150f, 150f, 150f);
    

    void Start() {
        potions = DataLoader.potions;
        
        recipeIcons = new Image[3];
        for (int i = 0; i < recipeIcons.Length; i++) {
            recipeIcons[i] = transform.Find("Icon" + (i+1)).GetComponent<Image>();
        }
        
        recipeIngredientText = new TextMeshProUGUI[3];
        for (int i = 0; i < recipeIngredientText.Length; i++) {
            recipeIngredientText[i] = transform.Find("IngredientText" + (i+1)).GetComponent<TextMeshProUGUI>();
        }
        
        ingredientQuantityText = new TextMeshProUGUI[3];
        for (int i = 0; i < ingredientQuantityText.Length; i++) {
            ingredientQuantityText[i] = transform.Find("QuantityText" + (i+1)).GetComponent<TextMeshProUGUI>();
        }

        brewItButton = transform.Find("BrewItButton").GetComponent<Image>();

        TurnToPage(0);
    }

    /*private void OnEnable() {
        potions = DataLoader.potions;
        TurnToPage(0);
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    public int FlipPage(bool fwd) {
        if (fwd) {
            for (int i = pageIndex+1; i < potions.Count; i++) {
                if (potions[i].isCraftable) return i;
            }
        }
        else {
            for (int i = pageIndex-1; i >= 0; i--) {
                if (potions[i].isCraftable) return i;
            }
        }

        return -1;
    }

    public void TurnToPage(int index) {
        Debug.Log("Setting recipe book page to: " + potions[index].potion_name + " (Page -)");
        Debug.Log("Found " + potions[index].recipe.Count + " ingredients in the recipe");
        for (int i = 0; i < potions[index].recipe.Count; i++) {
            Debug.Log("Setting ingredient " + potions[index].recipe[i].ingredient.ingredient_name);
            recipeIcons[i].sprite = Resources.Load<Sprite>("Art/UI/Ingredients/" + potions[index].recipe[i].ingredient.ingredient_name);
            recipeIcons[i].enabled = true;
            recipeIngredientText[i].SetText(potions[index].recipe[i].ingredient.ingredient_name);
            recipeIngredientText[i].enabled = true;
            ingredientQuantityText[i].SetText(potions[index].recipe[i].ingredient.quantityInInventory + "/" + potions[index].recipe[i].quantity);
            ingredientQuantityText[i].enabled = true;
        }

        if (potions[index].recipe.Count < 3) {
            for (int i = potions[index].recipe.Count; i < 3; i++) {
                recipeIcons[i].enabled = false;
                recipeIngredientText[i].enabled = false;
                ingredientQuantityText[i].enabled = false;
            }
        }
        
        //check if this potion can be crafted
        bool isBrewable = true;
        for (int i = 0; i < potions[index].recipe.Count; i++) {
            if (potions[index].recipe[i].ingredient.quantityInInventory < potions[index].recipe[i].quantity) { isBrewable = false; break; }
        }

        if (!isBrewable) {
            brewItButton.color = brewItButtonDisabledColor;
        }
        else {
            brewItButton.color = Color.white;
        }
    }
}
