using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    

    void Start()
    {
        //Camera.main = gameObject.GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        FindPlayer();
    }
    void FindPlayer()
    {
        PlayerMovement pm = PlayerMovement.Instance;
        if (pm)
        {
            transform.position = pm.transform.position + new Vector3(0, 0, -10f);
        }
    }
}
