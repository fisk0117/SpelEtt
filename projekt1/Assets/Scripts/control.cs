using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{

    public GameObject[] players;
    public int a = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");


        if (players.Length > 1)
        {
            a = 1;
        }
        else if (players.Length == 1)
        {
            a = 2;
        }
    }

    void OnGUI()
    {
        Vector2 nativeSize = new Vector2(640, 480);
        float posX = Screen.width / 2;
        float posY = Screen.height / 2;
        GUI.contentColor = Color.yellow;
        GUIStyle style = new GUIStyle();
        style.fontSize = (int)(20.0f * ((float)Screen.width / (float)nativeSize.x));

        if (a == 2)
        {
            GUI.Label(new Rect(posX - 100, posY - 10, 200, 20), "Waiting for players...", style);
        }
        else if (a == 1)
        {
            GUI.Label(new Rect(posX, posY, 200, 20), "", style);
        }

    }
}
