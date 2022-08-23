using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public float hp;
    public float starthp = 3;
    public bool death = false;

    public float deaths =0;
    public GameObject livingplayer;
    // Start is called before the first frame update
    void Start()
    {
        hp = starthp;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hp <= 0)
        {
            death = true;
        }
        if (death)
        {
               livingplayer.transform.position = new Vector3(0, 0, 0);
               hp = starthp;  
               death = false;
               deaths ++;
        }

       
    }

  
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
            hp -=1;
            
            
            
        }
    }
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "bullet")
    //     {
    //         Destroy(other.gameObject);
    //         hp--;
    //     }
    // }
}
