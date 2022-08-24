using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] players;
    int i;
    public Vector3[] playerspos;

    void Start()
    {
        playerspos = new Vector3[3];






        //Camera.main = gameObject.GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {

        players = GameObject.FindGameObjectsWithTag("Player");
        i = 1;

        foreach (GameObject Player in players)
        {
            playerspos[i] = Player.transform.position;
            i++;
        }


        transform.position = playerspos[1] + (playerspos[2] - playerspos[1]) / 2;

        float dist = Vector3.Distance(playerspos[1], playerspos[2]);
        
        transform.position = transform.position + new Vector3(0, 0, -20);
        if (transform.position.x != 0)
        {
            if (dist > 20)
            {
                Camera.main.orthographicSize = 20 + (dist-20);
            }

        }

        //FindPlayer();
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
