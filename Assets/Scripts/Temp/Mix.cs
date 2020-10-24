using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mix : MonoBehaviour
{
    public float circleSpeed; // the amount of seconds to complete a circle
    public float radius = 5;

    private Vector2 center;
    private float x, y;
    private float speed, angle;

    private bool mix;

    // Start is called before the first frame update
    void Start()
    {
        speed = (2 * Mathf.PI) / circleSpeed; //2*PI in degress is 360, so you get 5 seconds to complete a circle
        center = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (mix)
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

    private void OnMouseDown()
    {
        Debug.Log("Mixing...");
    }
}
