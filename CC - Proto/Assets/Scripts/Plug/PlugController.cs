using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugController : MonoBehaviour
{
    public int health = 100;
    public bool connected = false;
    public float explodeRange = 10f;
    public LayerMask whatIsEnemy;
    public int explosionDamage = 200;
    public Collider[] enemiesToDamage;

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

        if (connected)
        {
            if (Input.GetKeyDown("t") || Input.GetKeyDown("joystick button 3"))
            {
                Debug.Log("exploded!");
                enemiesToDamage = Physics.OverlapSphere(transform.position, explodeRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].tag == "enemy")
                    {
                        enemiesToDamage[i].GetComponent<EnemyHealth>().TakeMeleeDamage(explosionDamage);
                    }
                }
            }
        }
    }
}
