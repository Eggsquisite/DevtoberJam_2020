using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientGO : MonoBehaviour {
    
    public Ingredient ingredient;
    public ushort stackSize;
    
    private Image icon;
    private TextMeshProUGUI stackSizeText;

    void OnEnable() {
        icon = transform.Find("Icon").GetComponent<Image>();
        //Debug.Log("THIS WAS CALLED");
        //Debug.Log(icon.transform);
        stackSizeText = transform.Find("StackSize").GetComponent<TextMeshProUGUI>();
    }
    
    public void SetIngredient(Ingredient ingredient) {
        this.ingredient = ingredient;
        icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/" + ingredient.ingredient_name);
        
    }

    public void SetStackSize(ushort quantity) {
        stackSizeText.SetText(quantity + "");
    }
}
