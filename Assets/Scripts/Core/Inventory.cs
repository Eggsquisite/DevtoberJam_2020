using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public List<Ingredient> ingredients;

    public Inventory(List<Ingredient> ingredients) {
        this.ingredients = ingredients;
    }

    public void AddItem(Ingredient ingredient, ushort amount) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i] == ingredient) { ingredients[i].Add(amount); break; }
        }
    }

    public void RemoveItem(Ingredient ingredient, ushort amount) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i] == ingredient) { ingredients[i].Remove(amount); break; }
        }
    }

    public ushort CheckQuantity(Ingredient ingredient) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i] == ingredient) return ingredients[i].quantity;
        }
        return 0;
    }

    public override string ToString() {
        string s = "";
        for (int i = 0; i < ingredients.Count; i++) {
            s += ingredients[i].ingredient_name + "  (" + ingredients[i].quantity + ")\n";
        }
        return s;
    }
}
