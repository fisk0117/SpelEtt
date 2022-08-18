using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 input;
    public float speed = 10f;
    public float jumpForce = 1000f;
    void Update()
    {
        Movement();
    }
    void FixedUpdate()
    {
        rb.AddForce(input * Time.fixedDeltaTime);
    }
    void Movement()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0f);
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("yuh");
            rb.AddForce(gameObject.transform.up * jumpForce);
        }
    }
}
