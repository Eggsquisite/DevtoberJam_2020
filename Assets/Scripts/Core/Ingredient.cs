using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient {
    public string ingredient_name;
    public ushort cost;
    public ushort quantityInInventory = 0;
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
        quantityInInventory++; 
    }
    public void Add(ushort amount) {
        quantityInInventory += amount; 
    }

    public void Remove() {
        quantityInInventory--;
    }
    
    public void Remove(ushort amount) {
        quantityInInventory -= amount;
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

