using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Vials : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 originalPos;

    public enum vialColor { Red, Yellow, Blue };

    public vialColor m_color;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("Beginning Drag with color: " + m_color);
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        transform.position = originalPos;

        // If vial is over potion mix and can change color, change potion mix to appropriate color depending on vial
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Beaker")
            Debug.Log("On beaker");
    }
}
