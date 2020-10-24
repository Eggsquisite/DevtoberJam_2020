using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemType {
    POTIONS,
    INGREDIENTS
}

public class ItemSlot : MonoBehaviour, IDropHandler {
    
    public GameObject itemInSlot = null;
    public ItemType onlyAccepts;
    
    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<MoveableItem>().itemType == onlyAccepts) {

            if (itemInSlot == null) {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<MoveableItem>().slot.itemInSlot = null; //set the transferring slot to null
                itemInSlot = eventData.pointerDrag.gameObject;
                eventData.pointerDrag.GetComponent<MoveableItem>().slot = this;
            }
            //if this item slot is for ingredients and the slot isn't empty,
            //check if the ingredient in the slot matches the dragged ingredient so we can stack them
            else if (onlyAccepts == ItemType.INGREDIENTS && itemInSlot.GetComponent<IngredientGO>().ingredient == eventData.pointerDrag.GetComponent<IngredientGO>().ingredient) {
                itemInSlot.GetComponent<IngredientGO>().SetStackSize((ushort)(itemInSlot.GetComponent<IngredientGO>().stackSize + eventData.pointerDrag.GetComponent<IngredientGO>().stackSize));
                eventData.pointerDrag.GetComponent<MoveableItem>().slot.itemInSlot = null; //set the transferring slot to null
                Destroy(eventData.pointerDrag.gameObject);
            }
            
            
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
