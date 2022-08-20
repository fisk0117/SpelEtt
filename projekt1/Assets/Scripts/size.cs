using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class size : MonoBehaviour
{
    float a;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        a = player.GetComponent<Shooter>().ammocd;
        //Debug.Log(a);

        gameObject.active = true;
        transform.localScale = new Vector3((1-(a/3)), 0.2f ,1);
        

       
       
    }
}
