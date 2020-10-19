using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Start is called before the first frame update

    private Vector2 inputForce;
    private Rigidbody2D rb;
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    
    void Start() {
        inputForce = new Vector2();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Coding this manually to use different acceleration/deceleration values for the player
        if (horizontal != inputForce.x) {
            if (horizontal == 0f) inputForce.x = Mathf.Lerp(inputForce.x, horizontal, deceleration * Time.deltaTime);
            else inputForce.x = Mathf.Lerp(inputForce.x, horizontal, acceleration * Time.deltaTime);
        }
        if (vertical != inputForce.y) {
            if (vertical == 0f) inputForce.y = Mathf.Lerp(inputForce.y, vertical, deceleration * Time.deltaTime);
            else inputForce.y = Mathf.Lerp(inputForce.y, vertical, acceleration * Time.deltaTime);
        }
    }

    private void FixedUpdate() {
        Debug.Log(inputForce);
        rb.MovePosition(rb.position + (inputForce*maxSpeed* Time.fixedDeltaTime));

    }
}
