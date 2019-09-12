using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     //attack speed 
    public int attackDamage = 10;               //attack power / damage level


    Animator animate;          //reference animator
    GameObject player;         //reference player
    PlayerHealth playerHealth; //reference player health script to update hp when damage
    EnemyHealth enemyHealth;   //enemy health
    bool playerInRange;        //range
    float timer;                //snyc up attacks


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");   //reference the tag to player, do in awake to to conserve calls
        playerHealth = player.GetComponent <PlayerHealth> ();   //get the health of the player
        enemyHealth = GetComponent<EnemyHealth>();
        animate = GetComponent <Animator> ();
    }

    //use sphere trigger colider to detect enemy to player collision
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)  //check what to attack -- make sure its the player --
        {
            playerInRange = true;  //set in range to attack
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;  //set to no longer in range to attack
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;  //how much time has occured?

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();  //if its been long enough between attacks -- attack the player
        }

        if(playerHealth.currentHealth <= 0)
        {
            animate.SetTrigger ("PlayerDead");  //player was killed -- take enemy out of moving animation
        }
    }


    void Attack ()
    {
        timer = 0f;  //reset timer since we attacked

        if(playerHealth.currentHealth > 0)  //attack if the character has health left
        {
            playerHealth.TakeDamage (attackDamage);  //reference take damage
        }
    }
}
