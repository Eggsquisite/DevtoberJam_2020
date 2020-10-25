using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour {


    public List<Potion> potions;
    private int pageIndex;

    private Image[] recipeIcons;
    private TextMeshProUGUI[] recipeIngredientText, ingredientQuantityText;
    private TextMeshProUGUI potionTitle;
    private Button brewItButton, pageLeftButton, pageRightButton;

    void Start() {
        potions = Potion.potions;
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

        TurnToPage(0);
    }

    public void BrewIt() {
        //TODO
        brewItButton.GetComponent<AudioSource>().Play();
    }

    public void TurnPageRight() {
        Debug.Log("PageTurnRight Clicked");
        int nextPageIndex = FlipPage(pageIndex, true);
        pageRightButton.GetComponent<AudioSource>().Play();
        TurnToPage(nextPageIndex);
    }
    
    public void TurnPageLeft() {
        Debug.Log("PageTurnLeft Clicked");
        int nextPageIndex = FlipPage(pageIndex, false);
        pageLeftButton.GetComponent<AudioSource>().Play();
        TurnToPage(nextPageIndex);
    }

    public int FlipPage(int pageIndex, bool fwd) {
        if (fwd) {
            if (pageIndex + 1 > potions.Count) return -1;
            for (int i = pageIndex+1; i < potions.Count; i++) {
                if (potions[i].isCraftable) return i;
            }
        }
        else {
            if (pageIndex - 1 < 0) return -1;
            for (int i = pageIndex-1; i >= 0; i--) {
                if (potions[i].isCraftable) return i;
            }
        }

        return -1;
    }

    public void TurnToPage(int index) {
        Debug.Log("Setting recipe book page to: " + potions[index].potion_name + " (Page -)");
        potionTitle.SetText(potions[index].potion_name);
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
            brewItButton.interactable = false;
            Debug.Log("Unable to craft potion");
        }
        else {
            brewItButton.interactable = true;
        }
        
        //check to see if the page turn buttons can be used
        int check = FlipPage(index, true);
        if (check < 0) pageRightButton.interactable = false;
        else pageRightButton.interactable = true;
        check = FlipPage(index, false);
        if (check < 0) pageLeftButton.interactable = false;
        else pageLeftButton.interactable = true;

        pageIndex = index;
    }
}
