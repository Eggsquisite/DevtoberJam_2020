using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionGO : MonoBehaviour {
    
    public Potion potion;
    public Image icon;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetPotion(Potion potion) {
        this.potion = potion;
        icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/Bacon.png");
    }
}
