using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionGO : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler {
    
    public Potion potion;
    private Image icon;

    private TextMeshProUGUI itemName;
    //private GameObject patronWindow;

    void Start() {
        icon = transform.Find("Icon").GetComponent<Image>();
        itemName = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        //patronWindow = GameObject.Find("WindowFrame");
    }

    public void SetPotion(Potion potion) {
        this.potion = potion;
        //icon.sprite = Resources.Load<Sprite>("Art/UI/Ingredients/" + potion.potion_name);
        itemName.SetText(potion.potion_name);
    }
    
    void OnEnable() {
        icon = transform.Find("Icon").GetComponent<Image>();
        itemName = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemName.gameObject.SetActive(false);
        //patronWindow = GameObject.Find("WindowFrame");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        itemName.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        itemName.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        itemName.gameObject.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData) {
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name + "    " + patronWindow.name);

        if (eventData.pointerCurrentRaycast.gameObject.name == "WindowFrame") { //idk why doing gameObject == gameObject fails here
            Debug.Log("Trying to deliver potion");
            if (GameObject.Find("PatronManager").GetComponent<PatronManager>().GivePatronPotion(potion)) {
                //if true, they accepted the potion, so remove the gameobject
                Inventory.RemoveItem(potion);
                Debug.Log("DESTROYING");
                Destroy(this.gameObject);
            }
        }
        
    }
}
