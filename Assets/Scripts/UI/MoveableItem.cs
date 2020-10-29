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

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        //Debug.Log("Blah");
    }

    public void OnDrag(PointerEventData eventData) {
        //rectTransform.anchoredPosition += (eventData.delta / canvas.scaleFactor);
        this.transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;
        GetComponent<Canvas>().sortingOrder = 2;
    }

    public void OnEndDrag(PointerEventData eventData) {

        GameObject go = eventData.pointerDrag;
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>() == null || eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>().onlyAccepts != itemType) {
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
            rectTransform.anchoredPosition = Vector2.zero;
        }
        if (eventData.pointerDrag) canvasGroup.blocksRaycasts = true;
        GetComponent<Canvas>().sortingOrder = 1;
    }
}
