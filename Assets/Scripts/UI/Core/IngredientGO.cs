using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngredientGO : MonoBehaviour, IBeginDragHandler, IEndDragHandler {
    
    public Ingredient ingredient;
    public ushort stackSize;
    
    private Image icon;
    private TextMeshProUGUI itemName, stackSizeText;

    void OnEnable() {
        icon = transform.Find("Icon").GetComponent<Image>();
        //Debug.Log("THIS WAS CALLED");
        //Debug.Log(icon.transform);
        itemName = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        stackSizeText = transform.Find("StackSize").GetComponent<TextMeshProUGUI>();
    }
    
    public void SetIngredient(Ingredient ingredient) {
        this.ingredient = ingredient;
        icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/" + ingredient.ingredient_name);
        itemName.SetText(ingredient.ingredient_name);
    }

    public void SetStackSize(ushort quantity) {
        stackSizeText.SetText(quantity + "");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        stackSizeText.gameObject.SetActive(false);
        itemName.gameObject.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData) {
        stackSizeText.gameObject.SetActive(true);
        itemName.gameObject.SetActive(true);
    }
}
