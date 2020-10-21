using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient {
    public string ingredient_name;
    public ushort cost;
    public ushort quantity = 0;
    private Image icon;

    public Ingredient(string ingredient_name, ushort cost) {
        this.ingredient_name = ingredient_name;
        this.cost = cost;
        //icon = GetComponent<Image>();
        //icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/Bacon.png");
    }

    /*private void Start() {
        icon = GetComponent<Image>();
        icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/Bacon");
    }*/

    public void Add() {
        quantity++; 
    }
    public void Add(ushort amount) {
        quantity += amount; 
    }

    public void Remove() {
        quantity--;
    }
    
    public void Remove(ushort amount) {
        quantity -= amount;
    }
}

public struct WeightedIngredient {

    public Ingredient ingredient;
    public ushort quantity;

    public WeightedIngredient(Ingredient ingredient, ushort quantity) {
        this.ingredient = ingredient;
        this.quantity = quantity;
    }
}

