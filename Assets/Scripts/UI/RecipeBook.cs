using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour {


    public List<Potion> potions;
    private int pageIndex;

    private Image[] recipeIcons;
    private TextMeshPro[] recipeIngredientText;
    private TextMeshPro[] ingredientQuantityText;
    

    void Start() {
        potions = DataLoader.potions;
        
        recipeIcons = new Image[3];
        for (int i = 0; i < recipeIcons.Length; i++) {
            recipeIcons[i] = transform.Find("Icon" + (i+1)).GetComponent<Image>();
        }
        
        recipeIngredientText = new TextMeshPro[3];
        for (int i = 0; i < recipeIngredientText.Length; i++) {
            recipeIngredientText[i] = transform.Find("IngredientText" + (i+1)).GetComponent<TextMeshPro>();
        }
        
        ingredientQuantityText = new TextMeshPro[3];
        for (int i = 0; i < ingredientQuantityText.Length; i++) {
            ingredientQuantityText[i] = transform.Find("QuantityText" + (i+1)).GetComponent<TextMeshPro>();
        }
    }

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

        for (int i = 0; i > potions[index].recipe.Count; i++) {
            
        }
    }
}
