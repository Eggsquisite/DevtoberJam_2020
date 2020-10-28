using System.Collections.Generic;
using UnityEngine;

public class Potion {
    
    public string potion_name;
    public List<WeightedAttribute> attributes = new List<WeightedAttribute>();
    public List<WeightedIngredient> recipe = new List<WeightedIngredient>();
    public bool isInRecipeBook = false;
    
    public static List<Potion> potions;

    public Potion() { }

    public Potion(string potion_name) {
        this.potion_name = potion_name;
    }

    public void AddAttribute(WeightedAttribute attribute) {
        attributes.Add(attribute);
    }

    public void AddRecipeIngredient(WeightedIngredient ingredient) {
        recipe.Add(ingredient);
    }

    public bool CanBeCrafted() {
        for (int i = 0; i < recipe.Count; i++) {
            if (recipe[i].quantity > Inventory.QuantityInInventory(recipe[i].ingredient)) return false;
        }
        return true;
    }

    public override string ToString() {
        string s = potion_name + "\nAttributes:";
        for (int i = 0; i < attributes.Count; i++) {
            s += ("\n" + attributes[i].attribute.attribute_name + ": " + attributes[i].weight);
        }

        s += "\nRecipe:";
        for (int i = 0; i < recipe.Count; i++) {
            s += ("\n" + recipe[i].ingredient.ingredient_name + " (" + recipe[i].quantity + ")");
        }
        return s;
    }

    public static Potion FindPotion(string potion_name) {
        for (int i = 0; i < potions.Count; i++) {
            if (potions[i].potion_name == potion_name) return potions[i];
        }
        Debug.LogError("Unable to locate the potion '" + potion_name + "' from the PatronInput file");
        return null;
    }
}
