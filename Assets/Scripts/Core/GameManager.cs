﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    private Inventory inventory;
    
    void Start()
    {
        DataLoader.LoadDataFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
