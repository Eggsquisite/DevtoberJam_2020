using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mix : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public float circleSpeed; // the amount of seconds to complete a circle
    public float radius = 5;

    private Vector2 center;
    private float x, y;
    private float speed, angle;

    private bool mix, mouseClick, mouseEnter;

    // Start is called before the first frame update
    void Start()
    {
        speed = (2 * Mathf.PI) / circleSpeed; //2*PI in degress is 360, so you get 5 seconds to complete a circle
        center = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        // Add a buffer when mouse gets off mixer
        if (mouseClick && mouseEnter)
            Mixing();
    }

    void Mixing()
    {
        speed = (2 * Mathf.PI) / circleSpeed; //2*PI in degress is 360, so you get 5 seconds to complete a circle
        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=

        x = Mathf.Cos(angle) * radius + center.x;
        y = Mathf.Sin(angle) * radius + center.y;
        transform.position = new Vector2(x, y);
    }

    public void MixStatus(bool status)
    {
        mix = status;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseClick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseClick = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEnter = false;
    }
}
