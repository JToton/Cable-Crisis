using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private float timeBetweenSwings;
    public float startTimeBetweenSwings;

    AudioClip meleeAudio;                           //Audio source

    public int attackDamage = 10;               //attack power / damage level
    public Transform attackPosition;
    public LayerMask whatIsEnemy;
    public float attackRange;

    public float trigger;

    /*
    void Awake()
    {
        meleeAudio = GetComponent<AudioClip>();
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trigger = Input.GetAxis("Triggers");

        if (timeBetweenSwings <= 0){

            if (Input.GetKey(KeyCode.Space) || trigger < 0) {
                Collider[] enemiesToDamage = Physics.OverlapSphere(attackPosition.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].tag == "enemy")
                    {
                        enemiesToDamage[i].GetComponent<EnemyHealth>().TakeMeleeDamage(attackDamage);
                    }
                }
            }
            timeBetweenSwings = startTimeBetweenSwings;
        }
        else{
            timeBetweenSwings -= Time.deltaTime;
        }
    }
}
