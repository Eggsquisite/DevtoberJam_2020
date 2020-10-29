using UnityEngine;

public class GameManager : MonoBehaviour {
    
    //public Inventory inventory;
    public GameObject potionGO;
    public GameObject ingredientGO;
    public GameObject potionsParent;
    public GameObject ingredientsParent;

    private GameObject[] inventorySlots;
    private GameObject[] potionSlots;
    
    void Awake() {

        DataLoader.LoadDataFromFile();
        PatronLoader.LoadDataFromFile();

        potionGO = Resources.Load<GameObject>("Prefabs/Potion");
        ingredientGO = Resources.Load<GameObject>("Prefabs/Ingredient");
        
        potionsParent = GameObject.Find("Potions");
        ingredientsParent = GameObject.Find("Ingredients");
        
        inventorySlots = new GameObject[18];
        for (int i = 0; i < inventorySlots.Length; i++) {
            inventorySlots[i] = GameObject.Find("Slot" + (i+1));
        }
        
        potionSlots = new GameObject[3];
        for (int i = 0; i < potionSlots.Length; i++) {
            potionSlots[i] = GameObject.Find("PotionSlot" + (i+1));
        }

        int count = 0;
        //now instantiate the inventory
        for (int i = 0; i < Inventory.ingredientsInInventory.Count; i++) {
            if (Inventory.ingredientsInInventory[i].quantity > 0) {
                Debug.Log("Instantiating " + Inventory.ingredientsInInventory[i].ingredient.ingredient_name);
                GameObject newIngredient = Instantiate(ingredientGO, new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0f), Quaternion.identity);

                newIngredient.GetComponent<IngredientGO>().SetIngredient(Inventory.ingredientsInInventory[i].ingredient);
                newIngredient.GetComponent<IngredientGO>().SetStackSize(Inventory.ingredientsInInventory[i].quantity);

                inventorySlots[count].GetComponent<ItemSlot>().AddItemToSlot(newIngredient);
                count++;
            }
        }

        GameObject newPotion = Instantiate(potionGO, new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f), Quaternion.identity);
        newPotion.GetComponent<PotionGO>().SetPotion(Potion.potions[0]);
        potionSlots[0].GetComponent<ItemSlot>().AddItemToSlot(newPotion);
    }
}
