using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugController : MonoBehaviour
{
    public int health = 100;
    public bool connected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            //Destroy(gameObject);
        }
    }
}
