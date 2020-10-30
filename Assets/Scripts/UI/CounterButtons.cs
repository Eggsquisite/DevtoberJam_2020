using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterButtons : MonoBehaviour {


    private GameObject recipeBookGO, inventoryGO;
    private Button recipeBookButton, inventoryButton;

    
    void Start() {

        recipeBookGO = GameObject.Find("Recipe Book");
        inventoryGO = GameObject.Find("Inventory");
        
        recipeBookButton = transform.Find("RecipeBookButton").GetComponent<Button>();
        recipeBookButton.onClick.AddListener(RecipeBookClicked);
        inventoryButton = transform.Find("InventoryButton").GetComponent<Button>();
        inventoryButton.onClick.AddListener(InventoryClicked);
        
        recipeBookGO.SetActive(false);
        inventoryGO.SetActive(false);
    }

    public void RecipeBookClicked() {
        if (recipeBookGO.activeSelf) {
            recipeBookGO.SetActive(false);
        }
        else {
            inventoryGO.SetActive(false);
            recipeBookGO.SetActive(true);
        }
    }

    public void InventoryClicked() {
        if (inventoryGO.activeSelf) inventoryGO.SetActive(false);
        else {
            recipeBookGO.SetActive(false);
            inventoryGO.SetActive(true);
        }
    }

}
