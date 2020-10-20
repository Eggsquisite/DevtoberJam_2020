using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientGO : MonoBehaviour {
    
    public Ingredient ingredient;
    public Image icon;
    public Text text;

    void Start() {
        icon = transform.Find("Icon").GetComponent<Image>();
        text = transform.Find("StackSize").GetComponent<Text>();
    }
    
    void SetIngredient(Ingredient ingredient) {
        this.ingredient = ingredient;
        icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/" + ingredient.ingredient_name);
        text.text = ingredient.quantity + "";
    }
}
