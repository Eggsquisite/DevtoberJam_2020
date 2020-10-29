using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public static List<WeightedIngredient> ingredientsInInventory = new List<WeightedIngredient>();
    public static List<Potion> potionsInInventory = new List<Potion>(3);
    public static int money = 0;

    public delegate void InventoryEvent();
    public static event InventoryEvent InventoryChanged;

    public static void AddItem(Ingredient ingredient, ushort amount) {

        WeightedIngredient wi = FindIngredient(ingredient);
        if (wi != null) wi.quantity += amount;
        else ingredientsInInventory.Add(new WeightedIngredient(ingredient,amount));
        if (InventoryChanged != null) InventoryChanged();
    }
    
    public static void AddItem(Potion potion) {

        potionsInInventory.Add(potion);
        if (InventoryChanged != null) InventoryChanged();
    }

    public static void RemoveItem(Ingredient ingredient, ushort amount) {
        WeightedIngredient wi = FindIngredient(ingredient);
        if (wi != null) {
            wi.quantity -= amount;
            if (wi.quantity <= 0) {
                for (int i = 0; i < ingredientsInInventory.Count; i++) {
                    if (wi == ingredientsInInventory[i]) ingredientsInInventory.RemoveAt(i);
                }
            }
        }
        else ingredientsInInventory.Add(new WeightedIngredient(ingredient,amount));
        if (InventoryChanged != null) InventoryChanged();
    }

    public static void RemoveItem(Potion potion) {
        for (int i = 0; i < potionsInInventory.Count; i++) {
            if (potionsInInventory[i] == potion) { potionsInInventory.RemoveAt(i); break; }
        }
        if (InventoryChanged != null) InventoryChanged();
    }

    public static ushort QuantityInInventory(Ingredient ingredient) {
        WeightedIngredient wi = FindIngredient(ingredient);
        if (wi != null) return wi.quantity;
        else return 0;
    }

    public static WeightedIngredient FindIngredient(Ingredient ingredient) {
        for (int i = 0; i < ingredientsInInventory.Count; i++) {
            if (ingredientsInInventory[i].ingredient == ingredient) return ingredientsInInventory[i];
        }

        return null;
    }

    public static string ToString() {
        string s = "Money: " + money + "\n";
        for (int i = 0; i < ingredientsInInventory.Count; i++) {
            s += ingredientsInInventory[i].ingredient.ingredient_name + "  (" + ingredientsInInventory[i].quantity + ")\n";
        }
        return s;
    }

    public static void AddFunds(int funds) {
        money += funds;
        if (InventoryChanged != null) {
            InventoryChanged();
        }
    }
}
