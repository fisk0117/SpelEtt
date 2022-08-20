using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public static PlayerMovement Instance;

    public Rigidbody2D rb;
    Vector2 input;
    public float speed = 10f;
    public float jumpForce = 1000f;
    public float maxVelocity;
    public int jumpcd = 1;


    [Header("Grapple")]
    public LineRenderer _lindeRenderer;
    public DistanceJoint2D _distancejoint;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        _distancejoint.enabled = false;

    }
    void Update()
    {
        if (!isLocalPlayer) return;

        
        Instance = this;
        Grapple();
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
    void Grapple()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    _lindeRenderer.SetPosition(0, mousePos);
                    _lindeRenderer.SetPosition(1, transform.position);
                    _distancejoint.connectedAnchor = mousePos;
                    _distancejoint.enabled = true;
                    _lindeRenderer.enabled = true;
                }
            }
            
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _distancejoint.enabled = false;
            _lindeRenderer.enabled = false;
        }
        if (_distancejoint.enabled)
        {
            _lindeRenderer.SetPosition(1, transform.position);
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

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "mark")
        {
            if (jumpcd == 0)
            {
                jumpcd = 1;
            }

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
