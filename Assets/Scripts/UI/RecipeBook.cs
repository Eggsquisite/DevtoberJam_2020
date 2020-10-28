using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour {


    public static List<Potion> potionsInRecipeBook = new List<Potion>();
    private int pageIndex;

    private Image[] recipeIcons;
    private TextMeshProUGUI[] recipeIngredientText, ingredientQuantityText;
    private TextMeshProUGUI potionTitle;
    private Button brewItButton, pageLeftButton, pageRightButton;

    private Color32 quantityGood, quantityBad = new Color32(255,0,0,255);

    void Start() {
        //potions = Potion.potions;
        potionTitle = transform.Find("PotionTitle").GetComponent<TextMeshProUGUI>();
        
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

        brewItButton = transform.Find("BrewItButton").GetComponent<Button>();
        brewItButton.onClick.AddListener(BrewIt);
        pageLeftButton = transform.Find("PageLeftButton").GetComponent<Button>();
        pageLeftButton.onClick.AddListener(TurnPageLeft);
        pageRightButton = transform.Find("PageRightButton").GetComponent<Button>();
        pageRightButton.onClick.AddListener(TurnPageRight);

        quantityGood = ingredientQuantityText[0].color;

        TurnToPage(0);
    }

    public void BrewIt() {
        //TODO
        brewItButton.GetComponent<AudioSource>().Play();
    }

    public void TurnPageRight() {
        pageRightButton.GetComponent<AudioSource>().Play();
        TurnToPage(pageIndex+1);
    }
    
    public void TurnPageLeft() {
        pageLeftButton.GetComponent<AudioSource>().Play();
        TurnToPage(pageIndex-1);
    }

    public void TurnToPage(int index) {
        Debug.Log("Setting recipe book page to: " + potionsInRecipeBook[index].potion_name + " (Page -)");
        potionTitle.SetText(potionsInRecipeBook[index].potion_name);
        Debug.Log("Found " + potionsInRecipeBook[index].recipe.Count + " ingredients in the recipe");
        for (int i = 0; i < potionsInRecipeBook[index].recipe.Count; i++) {
            Debug.Log("Setting ingredient " + potionsInRecipeBook[index].recipe[i].ingredient.ingredient_name);
            recipeIcons[i].sprite = Resources.Load<Sprite>("Art/UI/Ingredients/" + potionsInRecipeBook[index].recipe[i].ingredient.ingredient_name);
            recipeIcons[i].enabled = true;
            recipeIngredientText[i].SetText(potionsInRecipeBook[index].recipe[i].ingredient.ingredient_name);
            recipeIngredientText[i].enabled = true;
            ingredientQuantityText[i].SetText(Inventory.QuantityInInventory(potionsInRecipeBook[index].recipe[i].ingredient) + "/" + potionsInRecipeBook[index].recipe[i].quantity);
            ingredientQuantityText[i].enabled = true;
        }

        if (potionsInRecipeBook[index].recipe.Count < 3) {
            for (int i = potionsInRecipeBook[index].recipe.Count; i < 3; i++) {
                recipeIcons[i].enabled = false;
                recipeIngredientText[i].enabled = false;
                ingredientQuantityText[i].enabled = false;
            }
        }
        
        //check if this potion can be crafted
        bool isBrewable = true;
        for (int i = 0; i < potionsInRecipeBook[index].recipe.Count; i++) {
            if (Inventory.QuantityInInventory(potionsInRecipeBook[index].recipe[i].ingredient) < potionsInRecipeBook[index].recipe[i].quantity) {
                ingredientQuantityText[i].color = quantityBad;
                isBrewable = false;
                break;
            }
            else ingredientQuantityText[i].color = quantityGood;
        }

        if (!isBrewable) {
            brewItButton.interactable = false;
            Debug.Log("Unable to craft potion");
        }
        else {
            brewItButton.interactable = true;
        }
        
        //check to see if the page turn buttons can be used
        
        if (pageIndex+1 > potionsInRecipeBook.Count-1) pageRightButton.interactable = false;
        else pageRightButton.interactable = true;
        if (pageIndex-1 < 0) pageLeftButton.interactable = false;
        else pageLeftButton.interactable = true;

        pageIndex = index;
    }
}
