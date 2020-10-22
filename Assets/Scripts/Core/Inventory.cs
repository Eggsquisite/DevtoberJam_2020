using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public static List<Ingredient> ingredients;

    /*public Inventory(List<Ingredient> ingredients) {
        Inventory.ingredients = ingredients;
    }*/

    public static void AddItem(Ingredient ingredient, ushort amount) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i] == ingredient) { ingredients[i].Add(amount); break; }
        }
    }

    public static void RemoveItem(Ingredient ingredient, ushort amount) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i] == ingredient) { ingredients[i].Remove(amount); break; }
        }
    }

    public static ushort CheckQuantity(Ingredient ingredient) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i] == ingredient) return ingredients[i].quantityInInventory;
        }
        return 0;
    }

    /*public static Ingredient FindIngredient(Ingredient ingredient) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i] == ingredient) return 
        }
    }*/

    public static string ToString() {
        string s = "";
        for (int i = 0; i < ingredients.Count; i++) {
            s += ingredients[i].ingredient_name + "  (" + ingredients[i].quantityInInventory + ")\n";
        }
        return s;
    }
}
