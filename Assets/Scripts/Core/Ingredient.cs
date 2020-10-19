using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour {
    public string ingredient_name;
    public ushort cost;
    public ushort quantity = 0;
    private Image icon;

    public Ingredient(string ingredient_name, ushort cost) {
        this.ingredient_name = ingredient_name;
        this.cost = cost;
        icon = GetComponent<Image>();
        icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/Bacon.png");
    }

    private void Start() {
        icon = GetComponent<Image>();
        icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/Bacon");
    }

    public void AddOne() {
        quantity++; 
    }

    public void RemoveOne() {
        quantity--;
    }

}

