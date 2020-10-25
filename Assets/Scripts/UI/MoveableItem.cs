using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public ItemType itemType;
    public ItemSlot slot;
    private GameObject stackSize, itemName;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        stackSize = transform.Find("StackSize").gameObject;
        itemName = transform.Find("ItemName").gameObject;
    }

    public void OnPointerDown(PointerEventData eventData) {
        //Debug.Log("Blah");
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += (eventData.delta / canvas.scaleFactor);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;
        stackSize.SetActive(false);
        itemName.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (eventData.pointerDrag)
        canvasGroup.blocksRaycasts = true;
        stackSize.SetActive(true);
        itemName.SetActive(true);
    }
}
