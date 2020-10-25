using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public static List<Ingredient> ingredients;
    public static List<Potion> potions;
    public static int money = 0;

    public delegate void MoneyEvent();
    public static event MoneyEvent MoneyChangedListener;

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
        string s = "Money: " + money + "\n";
        for (int i = 0; i < ingredients.Count; i++) {
            s += ingredients[i].ingredient_name + "  (" + ingredients[i].quantityInInventory + ")\n";
        }
        return s;
    }

    public static void AddFunds(int funds) {
        money += funds;
        if (MoneyChangedListener != null) MoneyChangedListener();
    }
}
