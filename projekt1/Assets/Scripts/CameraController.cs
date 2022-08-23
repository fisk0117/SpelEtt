using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] players;
    


    void Start()
    {
        if (players == null)
            players = GameObject.FindGameObjectsWithTag("Player");

        //Camera.main = gameObject.GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        //foreach (GameObject Player in players)
        FindPlayer();
    }
    void FindPlayer()
    {
        PlayerMovement pm = PlayerMovement.Instance;
        if (pm)
        {
            transform.position = pm.transform.position + new Vector3(0, 0, -30f);
        }
    }
}
