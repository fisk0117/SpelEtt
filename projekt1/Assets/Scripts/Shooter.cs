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
        
        else if ((ammo <= 0) && (ammocdcd == 0)){
            ammocd = ammocdroof;
            ammocdcd = 1;

        }

        if (ammocd >=0)
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
        ShootOnServer();
    }

    [Command(requiresAuthority = false)]
    void ShootOnServer()
    {
        ShootOnClient();
    }

    [ClientRpc]
    void ShootOnClient()
    {
        GameObject lazerBullet = Instantiate(bullet, shootPos.position, transform.rotation);
        Physics2D.IgnoreCollision(lazerBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        lazerBullet.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        lazerBullet.GetComponent<Rigidbody2D>().AddForce(shootPos.right * speed);
        gameObject.GetComponent<Rigidbody2D>().AddForce(shootPos.right * -speed * kb);
    }

}
