using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorRaycast : MonoBehaviour
{
    [SerializeField] GraphicRaycaster m_Raycaster;
    [SerializeField] EventSystem m_EventSystem;
    PointerEventData m_PointerEventData;

    private void Start()
    {
        
    }

    void Update()
    {
        //Ray castPoint = Camera.main.ScreenPointToRay(mousePos);

        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the cursor
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

            Debug.Log("hi");

        if (results.Count > 0)
        {
            Debug.Log(results[0].gameObject.name + " Was Clicked");
        }
    }

}
