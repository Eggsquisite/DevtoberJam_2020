using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public Inventory inventory;
    public GameObject potionGO;
    public GameObject ingredientGO;
    public GameObject potionsParent;
    public GameObject ingredientsParent;
    
    void Start() {
        DataLoader.LoadDataFromFile();
        inventory = DataLoader.inventory;

        potionGO = Resources.Load<GameObject>("Prefabs/Potion");
        ingredientGO = Resources.Load<GameObject>("Prefabs/Ingredient");
        
        potionsParent = GameObject.Find("Potions");
        ingredientsParent = GameObject.Find("Ingredients");
        
        //now instantiate the inventory
        for (int i = 0; i < inventory.ingredients.Count; i++) {
            if (inventory.ingredients[i].quantity > 0) {
                Debug.Log("Instantiating " + inventory.ingredients[i].ingredient_name);
                GameObject newIngredient = Instantiate(ingredientGO, new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0f), Quaternion.identity);
                newIngredient.GetComponent<IngredientGO>().SetIngredient(inventory.ingredients[i]);
                newIngredient.transform.parent = ingredientsParent.transform;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
