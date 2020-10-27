using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient {
    public string ingredient_name;
    public ushort cost;
    //public ushort quantityInInventory = 0;
    private Image icon;

    public static List<Ingredient> ingredients;

    public Ingredient(string ingredient_name, ushort cost) {
        this.ingredient_name = ingredient_name;
        this.cost = cost;
    }

    /*
    public void Add(ushort amount) {
        quantityInInventory += amount; 
    }

    public void Remove(ushort amount) {
        quantityInInventory -= amount;
    }*/
    
    public static Ingredient FindIngredient(string ingredient) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i].ingredient_name == ingredient) return ingredients[i];
        }
        Debug.LogError("We were unable to find the ingredient '" + ingredient + "'.  Is this a typo?");
        return null;
    }
}

public class WeightedIngredient {

    public Ingredient ingredient;
    public ushort quantity;

    public WeightedIngredient(Ingredient ingredient, ushort quantity) {
        this.ingredient = ingredient;
        this.quantity = quantity;
    }
}

