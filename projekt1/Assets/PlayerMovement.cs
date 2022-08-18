using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public Rigidbody2D rb;
    Vector2 input;
    public float speed = 10f;
    public float jumpForce = 1000f;
    public float maxVelocity;
    public int jumpcd = 1;
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Movement();
    }
    void FixedUpdate()
    {
        rb.AddForce(input * Time.fixedDeltaTime);
    }
    void Movement()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0f);


        if (Input.GetKeyDown("space") && (jumpcd == 1))
        {
            jumpcd = 0;
            Debug.Log("yuh");
            rb.AddForce(gameObject.transform.up * jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "mark")
        {
            jumpcd = 1;
            Debug.Log("mark");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "mark")
        {
            jumpcd = 0;
        }
    }
}
