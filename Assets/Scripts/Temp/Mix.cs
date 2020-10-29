using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mix : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public float baseCircleSpeed; // the amount of seconds to complete a circle
    public float radius;
    public float slowDownSpeed;

    public Transform beaker;
    private Vector2 center;
    private float x, y;
    private float speed, angle, tmpCircleSpeed;

    private bool mix, mouseClick, mouseEnter;

    // Start is called before the first frame update
    void Start()
    {
        tmpCircleSpeed = baseCircleSpeed;

        speed = (2 * Mathf.PI) / baseCircleSpeed; //2*PI in degress is 360, so you get 5 seconds to complete a circle

        CalculatePos();
        transform.position = new Vector2(x, y);
    }

    void Update()
    {
        // Add a buffer when mouse gets off mixer
        if (mouseClick && mouseEnter)
            Mixing();
        else if (mix)
            StopMixing();
    }

    void Mixing()
    {
        if (baseCircleSpeed == 0)
            return;

        mix = true;
        tmpCircleSpeed = baseCircleSpeed;
        speed = (2 * Mathf.PI) / baseCircleSpeed; //2*PI in degress is 360, so you get 5 seconds to complete a circle
        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=

        CalculatePos();
        transform.position = new Vector2(x, y);
    }

    void StopMixing()
    {
        if (tmpCircleSpeed >= baseCircleSpeed * 4)
        {
            mix = false;
            return;
        }

        tmpCircleSpeed += Time.deltaTime / slowDownSpeed;
        speed = (2 * Mathf.PI) / tmpCircleSpeed;

        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=

        CalculatePos();
        transform.position = new Vector2(x, y);
    }

    private void CalculatePos()
    {
        center = new Vector2(beaker.position.x, beaker.position.y);
        x = Mathf.Cos(angle) * radius + center.x;
        y = Mathf.Sin(angle) * radius + center.y;
    }

    public bool GetMixStatus() {
        return mix;
    }

    public void OnPointerDown(PointerEventData eventData) {
        mouseClick = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        mouseClick = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouseEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseEnter = false;
    }
}
