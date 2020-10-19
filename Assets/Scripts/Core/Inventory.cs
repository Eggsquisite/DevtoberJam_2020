using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Ingredient> ingredients;
    
    void Start() {
        
    }

    
    void Update() {
        
    }

    void AddItem(Ingredient ingredient) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i].name == ingredient.name) { ingredients[i].AddOne(); break; }
        }
    }

    void RemoveItem(Ingredient ingredient) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i].name == ingredient.name) { ingredients[i].RemoveOne(); break; }
        }
    }

    ushort CheckQuantity(Ingredient ingredient) {
        for (int i = 0; i < ingredients.Count; i++) {
            if (ingredients[i].name == ingredient.name) return ingredients[i].quantity;
        }
        return 0;
    }
}
