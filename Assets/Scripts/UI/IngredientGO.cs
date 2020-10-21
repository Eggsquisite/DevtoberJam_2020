using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientGO : MonoBehaviour {
    
    public Ingredient ingredient;
    public Image icon;
    public Text text;

    void OnEnable() {
        icon = transform.Find("Icon").GetComponent<Image>();
        //Debug.Log("THIS WAS CALLED");
        //Debug.Log(icon.transform);
        text = transform.Find("StackSize").GetComponent<Text>();
    }
    
    public void SetIngredient(Ingredient ingredient) {
        this.ingredient = ingredient;
        icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/" + ingredient.ingredient_name);
        text.text = ingredient.quantity + "";
    }
}
