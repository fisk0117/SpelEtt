using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scorescript : MonoBehaviour
{
    public GameObject[] players;
    public float[] score;



    int i;

    // Start is called before the first frame update
    void Start()
    {
        score = new float[10];
        score[1] = 0;
        score[2] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        i = 1;


        foreach (GameObject player in players)
        {
            score[i] = player.GetComponent<health>().deaths;
            i++;
        }
    }

    void OnGUI()
    {


        float posX = Screen.width / 2;
        float posY = Screen.height / 2;


        
        GUI.contentColor = Color.yellow;
        GUI.Label(new Rect(posX - 100, 0, 200, 20), score[1].ToString());
        GUI.contentColor = Color.yellow;
        GUI.Label(new Rect(posX + 100, 0, 200, 20), score[2].ToString());

        Debug.Log(score[1]);
        Debug.Log(score[2]);

    }
}
