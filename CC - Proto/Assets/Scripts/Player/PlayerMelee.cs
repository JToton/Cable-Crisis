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
        if (timeBetweenSwings <= 0){

            if (Input.GetKey(KeyCode.Space)) {
                Collider[] enemiesToDamage = Physics.OverlapSphere(attackPosition.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeMeleeDamage(attackDamage);
                }
            }
            timeBetweenSwings = startTimeBetweenSwings;
        }
        else{
            timeBetweenSwings -= Time.deltaTime;
        }
    }
    /*
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
    */
}
