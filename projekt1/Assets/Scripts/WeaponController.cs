using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Pisstol")]
    public GameObject Pistol;

    Vector3 mousePos;
    public Transform flRot;

    public float flSpeed = 5f;
    float angleOffset;
    float angle;

    Vector3 screenPosition;
    Vector3 mousePosition;
    bool on;

    private void Update()
    {
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

        angle = Vector2.SignedAngle(mousePos, Vector2.right) + angleOffset;

        flRot.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(flRot.eulerAngles.z, -angle, Time.deltaTime * flSpeed));
    }
}
