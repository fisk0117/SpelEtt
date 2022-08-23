using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public static PlayerMovement Instance;
    public GameObject livingplayer;
    public GameObject hp;
    public Rigidbody2D rb;
    Vector2 input;
    private GUIStyle red;
    public float speed = 10f;
    public float jumpForce = 1000f;
    public float maxVelocity;
    public int jumpcd = 1;
    public Texture ammotexture;
    float ammo;
    float life;

    [Header("Grapple")]
    public LineRenderer _lindeRenderer;
    public DistanceJoint2D _distancejoint;
    public GameObject grapple;
    public Transform shootPos;
    public float grapSpeed;
    GameObject grap;


    [SyncVar]
    public bool grapOn;

    private void Start()
    {
        grapOn = false;
        if (!isLocalPlayer)
        {
            hp.active = false;
            return;
        }
        

        _distancejoint.enabled = false;

    }
    void Update()
    {
        OnLineRenderer(grapOn);

        if (!isLocalPlayer) return;


        ammo = livingplayer.GetComponent<Shooter>().ammo;
        life = livingplayer.GetComponent<health>().hp;

        Instance = this;
        NewGrapple();
        Movement();

    }

    void OnGUI()
    {
        if (!isLocalPlayer) return;
        // Vector2 _target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        // Vector2 _myPos = shootPos.position;
        // Vector2 _direction = (_target - _myPos).normalized;
        float posX = Screen.width / 2;
        float posY = Screen.height / 2;
        // Vector3 pos = shootPos.position.normalized;
        GUI.contentColor = Color.black;
        for (float i = ammo; i > 0; i--){
            GUI.DrawTexture(new Rect(posX - 15 + i*7 , posY - 30, 5, 5), ammotexture, ScaleMode.ScaleToFit, true,0f);
        }
        //GUI.Label(new Rect(posX, posY - 30, 100, 20), ammo.ToString());
        GUI.contentColor = Color.red;
        GUI.Label(new Rect(posX, posY - 45, 100, 20), life.ToString());
        
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

    public void OnLineRenderer(bool on)
    {
        if (grap)
        {
            _lindeRenderer.SetPosition(0, grap.transform.position);
            _lindeRenderer.SetPosition(1, transform.position);
            _distancejoint.connectedAnchor = grap.transform.position;
            _lindeRenderer.enabled = true;
            if (grap.GetComponent<Rigidbody2D>().isKinematic)
            {
                _distancejoint.enabled = true;
            }
        }
    }

    void NewGrapple()
    {
        Vector2 _target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 _myPos = shootPos.position;
        Vector2 _direction = (_target - _myPos).normalized;
        Vector3 _rot = transform.rotation.eulerAngles;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GrapOnServer(_myPos, _direction, _rot);
        }
    }

    [Command(requiresAuthority = false)]
    void GrapOnServer(Vector2 _myPos, Vector2 _direction, Vector3 _rot)
    {
        GrapOnClient(_myPos, _direction, _rot);
    }

    [ClientRpc]
    void GrapOnClient(Vector2 _myPos, Vector2 _direction, Vector3 _rot)
    {
        if (grap)
        {
            grapOn = false;
            Destroy(grap);
            grap = null;
            _distancejoint.enabled = false;
            _lindeRenderer.enabled = false;
        }
        else
        {
            grap = Instantiate(grapple, shootPos.position, Quaternion.Euler(_rot));
            Physics2D.IgnoreCollision(grap.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            grap.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            grap.GetComponent<Rigidbody2D>().velocity = (_direction * grapSpeed);
            grapOn = true;

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
