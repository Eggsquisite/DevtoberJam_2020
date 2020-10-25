using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Vials : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 originalPos;
    private CanvasGroup canvasGroup;


    public enum vialColor { Red, Yellow, Blue };

    public vialColor m_color;
    private int realColor;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        canvasGroup = GetComponent<CanvasGroup>();

        if (m_color == vialColor.Red)           realColor = 0;
        else if (m_color == vialColor.Yellow)   realColor = 1;
        else if (m_color == vialColor.Blue)     realColor = 2;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("Beginning Drag with color: " + m_color);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        transform.position = originalPos;
        canvasGroup.blocksRaycasts = true;
        // If vial is over potion mix and can change color, change potion mix to appropriate color depending on vial
    }

    public int GetColor()
    {
        return realColor;
    }
}
