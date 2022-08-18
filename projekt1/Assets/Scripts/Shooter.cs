using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPos;
    public float speed;
    public float kb = 0.4f;
    public Color color;
    TrailRenderer tr;
    void Start()
    {
        tr = bullet.GetComponent<TrailRenderer>();
    }

    void Update()
    {
        tr.startColor = color;
        tr.endColor = color;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject lazerBullet = Instantiate(bullet, shootPos.position, transform.rotation);
        lazerBullet.GetComponent<Rigidbody2D>().AddForce(shootPos.right * speed);
        gameObject.GetComponent<Rigidbody2D>().AddForce(shootPos.right * -speed * kb);
    }
}
