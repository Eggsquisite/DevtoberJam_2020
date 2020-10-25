using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Beaker : MonoBehaviour, IDropHandler
{
    private enum vialColor { Red, Yellow, Blue };

    private vialColor m_color;

    public void OnDrop(PointerEventData data)
    {
        Debug.Log("OnDrop");
        if (data.pointerDrag != null)
        {
            Debug.Log(data.pointerDrag.GetComponent<Vials>().GetColor()); 
        }
            
    }
}
