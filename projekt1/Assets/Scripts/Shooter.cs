using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Shooter : NetworkBehaviour
{
    public GameObject bullet;
    public Transform shootPos;
    public float speed;
    public float kb = 0.4f;
    public Color color;
    TrailRenderer tr;
    public float ammo = 3f;
    public float ammocd = 0f;
    public float ammocdroof = 3f;
    public float ammocdcd = 0f;

    Vector3 mousePos;
    public Transform flRot;

    public float flSpeed = 5f;
    float angle;
    public float countdowndestroy = 2;
    Vector3 screenPosition;
    Vector3 mousePosition;
    void Start()
    {
        tr = bullet.GetComponent<TrailRenderer>();


    }

    void Update()
    {

        tr.startColor = color;
        tr.endColor = color;


        if (Input.GetMouseButtonDown(0) && (ammo >= 1) && (ammocd <= 0) && this.isLocalPlayer)
        {
            Shoot();
            ammo--;
            Debug.Log(ammo);
        }

        else if ((ammo <= 0) && (ammocdcd == 0))
        {
            ammocd = ammocdroof;
            ammocdcd = 1;

        }

        if (ammocd >= 0)
        {
            ammocd -= Time.deltaTime;

        }

        if ((ammocd <= 0) && (ammocdcd == 1))
        {
            ammo = 3f;
            ammocdcd = 0;
        }



    }
    void Shoot()
    {
        Vector2 _target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 _myPos = shootPos.position;
        Vector2 _direction = (_target - _myPos).normalized;
        Vector3 _rot = transform.rotation.eulerAngles;

        ShootOnServer(_myPos, _direction, _rot);
    }

    [Command(requiresAuthority = false)]
    void ShootOnServer(Vector2 _myPos, Vector2 _direction, Vector3 _rot)
    {
        ShootOnClient(_myPos, _direction, _rot);
    }

    [ClientRpc]
    void ShootOnClient(Vector2 _myPos, Vector2 _direction, Vector3 _rot)
    {




        GameObject lazerBullet = Instantiate(bullet, _myPos, Quaternion.Euler(_rot));
        Physics2D.IgnoreCollision(lazerBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        //lazerBullet.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        lazerBullet.GetComponent<Rigidbody2D>().velocity = (_direction * speed);
        gameObject.GetComponent<Rigidbody2D>().AddForce(_direction * -speed * kb);
        //Destroy(lazerBullet, countdowndestroy);
        //NetworkServer.Spawn(lazerBullet);





    }
}
