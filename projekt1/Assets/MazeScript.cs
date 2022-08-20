using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScript : MonoBehaviour
{
    public GameObject cell;
    public float width;
    public float height;
    public GameObject[,] grid;
    public bool[,] visited;


    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[(int)width, (int)height];
        visited = new bool[(int)width, (int)height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[x,y] = Instantiate(cell, new Vector3(x * 10f, y * 10f, 0f), Quaternion.identity);
            }
        }
    }
    void MazeGeneration()
    {
        int x = (int)Random.Range(0, width - 1);
        int y = (int)Random.Range(0, height - 1);
        GameObject current = grid[x, y];





    }
}
