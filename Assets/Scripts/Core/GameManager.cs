﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    //public Inventory inventory;
    public GameObject potionGO;
    public GameObject ingredientGO;
    public GameObject potionsParent;
    public GameObject ingredientsParent;

    private GameObject[] inventorySlots;
    
    void Awake() {
        DataLoader.LoadDataFromFile();
        // inventory = DataLoader.inventory;

        potionGO = Resources.Load<GameObject>("Prefabs/Potion");
        ingredientGO = Resources.Load<GameObject>("Prefabs/Ingredient");
        
        potionsParent = GameObject.Find("Potions");
        ingredientsParent = GameObject.Find("Ingredients");
        
        inventorySlots = new GameObject[18];
        for (int i = 0; i < inventorySlots.Length; i++) {
            inventorySlots[i] = GameObject.Find("Slot" + (i+1));
        }

        int count = 0;
        //now instantiate the inventory
        for (int i = 0; i < Inventory.ingredients.Count; i++) {
            if (Inventory.ingredients[i].quantityInInventory > 0) {
                Debug.Log("Instantiating " + Inventory.ingredients[i].ingredient_name);
                GameObject newIngredient = Instantiate(ingredientGO, new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0f), Quaternion.identity);
                //newIngredient.transform.localScale = Vector3.one;
                newIngredient.GetComponent<IngredientGO>().SetIngredient(Inventory.ingredients[i]);
                newIngredient.GetComponent<IngredientGO>().SetStackSize(Inventory.ingredients[i].quantityInInventory);
                //newIngredient.transform.parent = ingredientsParent.transform;
                //newIngredient.transform.SetParent(ingredientsParent.transform, false);
                
                inventorySlots[count].GetComponent<ItemSlot>().AddItemToSlot(newIngredient);
                count++;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
