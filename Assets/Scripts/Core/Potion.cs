using System.Collections.Generic;
using UnityEngine;

public class Potion {
    
    public string potion_name;
    public List<WeightedAttribute> attributes = new List<WeightedAttribute>();
    public List<WeightedIngredient> recipe = new List<WeightedIngredient>();
    public bool isCraftable = false;

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
}

public struct PotionSolution {
    public Potion potion { get; set; }
    public ushort payment { get; set; }
    public bool isDrunkImmediately { get; set; }
    public string comment { get; set; }
}
