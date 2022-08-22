using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class WeaponController : NetworkBehaviour
{
    [Header("Pisstol")]
    public GameObject Pistol;

    Vector3 mousePos;
    public Transform flRot;

    public float flSpeed = 5f;
    float angle;

    Vector3 screenPosition;
    Vector3 mousePosition;
    bool on;

    


    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }


    }


    private void Update()
    {
        if (!isLocalPlayer)
            return;
        PistolRotation();
    }
    void PistolRotation()
    {
        if (!Pistol)
        {
            return;
        }
        screenPosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);

        mousePos = mousePosition - transform.position;

        angle = Vector2.SignedAngle(mousePos, Vector2.right);
        flRot.eulerAngles = new Vector3(0f, 0f, -angle);
    }
}
