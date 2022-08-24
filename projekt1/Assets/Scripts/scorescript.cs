using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class scorescript : NetworkBehaviour
{
    public GameObject[] players;
    public float[] score;
    public float[] rounds;

    public Sprite spelare1;
    public Sprite spelare2;



    int i;

    public float vinst;

    // Start is called before the first frame update
    void Start()
    {
        score = new float[10];
        score[1] = 0;
        score[2] = 0;
        rounds = new float[3];
        rounds[1] = 0;
        rounds[2] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        i = 1;


        foreach (GameObject player in players)
        {
            score[i] = player.GetComponent<health>().deaths;
            if (i == 1)
            {
                SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();

                renderer.sprite = spelare1;
            }
            if (i == 2)
            {
                SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();

                renderer.sprite = spelare2;
            }
            i++;
        }

        if (score[1] >= 2)
        {
            vinst = 1;
            rounds[1] += 1;
            Invoke("vinst0", 3f);
            foreach (GameObject player in players)
            {
                player.GetComponent<health>().deaths = 0;

            }
            players[0].GetComponent<PlayerMovement>().cash = 1;



        }
        if (score[2] >= 2)
        {
            vinst = 2;
            rounds[2] += 1;
            Invoke("vinst0", 3f);

            foreach (GameObject player in players)
            {
                player.GetComponent<health>().deaths = 0;

            }
            players[1].GetComponent<PlayerMovement>().cash = 1;
        }


    }

    void vinst0()
    {
        vinst = 0;
    }

    void OnGUI()
    {

        Vector2 nativeSize = new Vector2(640, 480);
        GUIStyle style = new GUIStyle();
        style.fontSize = (int)(40.0f * ((float)Screen.width / (float)nativeSize.x));
        float posX = Screen.width / 2;
        float posY = Screen.height / 2;

        GUIStyle style2 = new GUIStyle();
        style2.fontSize = (int)(40.0f * ((float)Screen.width / (float)nativeSize.x));



        GUI.contentColor = Color.yellow;
        GUI.Label(new Rect(posX - 120, 0, 200, 20), score[1].ToString());

        GUI.Label(new Rect(posX + 80, 0, 200, 20), score[2].ToString());

        GUI.Label(new Rect(posX - 100, 0, 200, 20), rounds[1].ToString(), style2);

        GUI.Label(new Rect(posX + 100, 0, 200, 20), rounds[2].ToString(), style2);

        if (vinst == 1)
        {
            GUI.Label(new Rect(posX - 100, posY - 10, 200, 20), "Player 2 wins round", style);
        }
        if (vinst == 2)
        {
            GUI.Label(new Rect(posX - 100, posY - 10, 200, 20), "Player 1 wins round", style);
        }


    }
}
