using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public Inventory inventory;
    
    void Start() {
        DataLoader.LoadDataFromFile();
        inventory = DataLoader.inventory;
        
        //now instantiate the inventory
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
