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
        Debug.Log("Dropping into slot " + transform.name);
        if (eventData.pointerDrag.gameObject == itemInSlot) {
            Debug.Log("Returning item back to its slot");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<MoveableItem>().itemType == onlyAccepts) {

            if (itemInSlot == null) {
                Debug.Log("The Slot was empty.  Adding");
                AddItemToSlot(eventData.pointerDrag.gameObject);
            }
            //if this item slot is for ingredients and the slot isn't empty,
            //check if the ingredient in the slot matches the dragged ingredient so we can stack them
            else if (onlyAccepts == ItemType.INGREDIENTS && itemInSlot.GetComponent<IngredientGO>().ingredient == eventData.pointerDrag.GetComponent<IngredientGO>().ingredient) {
                Debug.Log("Adding to the stack.  Ingredients:  " + itemInSlot.GetComponent<IngredientGO>().ingredient.ingredient_name + "   " + eventData.pointerDrag.GetComponent<IngredientGO>().ingredient.ingredient_name);
                itemInSlot.GetComponent<IngredientGO>().SetStackSize((ushort)(itemInSlot.GetComponent<IngredientGO>().stackSize + eventData.pointerDrag.GetComponent<IngredientGO>().stackSize));
                if (eventData.pointerDrag.GetComponent<MoveableItem>().slot != null) { //this is probably always true, unless items come from somewhere that isn't a slot
                    eventData.pointerDrag.GetComponent<MoveableItem>().slot.itemInSlot = null; //set the transferring slot to null
                }
                Destroy(eventData.pointerDrag.gameObject);
            }
            else {
                Debug.Log("Moving item back to its slot");
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        //else move back to the original slot
        else {
            Debug.Log("Moving item back to its slot");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    public void AddItemToSlot(GameObject item) {
        item.transform.SetParent(transform, false);
        //item.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        if (item.GetComponent<MoveableItem>().slot != null) {
            item.GetComponent<MoveableItem>().slot.itemInSlot = null;
        }
        itemInSlot = item;
        item.GetComponent<MoveableItem>().slot = this;
    }
}
