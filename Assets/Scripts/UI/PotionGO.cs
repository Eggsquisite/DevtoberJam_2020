using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionGO : MonoBehaviour {
    
    public Potion potion;
    public Image icon;
    //public Text text;

    void Start() {
        icon = transform.Find("Icon").GetComponent<Image>();
        //text = transform.Find("StackSize").GetComponent<Text>();
    }

    void SetPotion(Potion potion) {
        this.potion = potion;
        icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/" + potion.potion_name);
    }
}
