using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    public int target;
    GameObject[] plugs1;
    GameObject[] plugs2;
    public PlugController nearestPlug;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();

        target = Random.Range(0, 2);
        plugs1 = GameObject.FindGameObjectsWithTag("battery");
        plugs2 = GameObject.FindGameObjectsWithTag("battery2");

        float minDistance = Mathf.Infinity;

        if(plugs1.Length > 0)
        {
            foreach (GameObject p in plugs1)
            {
                float distance = Vector3.Distance(p.transform.position, transform.position);
                if(distance < minDistance)
                {
                    nearestPlug = p.GetComponent<PlugController>();
                    minDistance = distance;
                }
            }
        }
        if(plugs2.Length > 0)
        {
            foreach (GameObject p in plugs2)
            {
                float distance = Vector3.Distance(p.transform.position, transform.position);
                if(distance < minDistance)
                {
                    nearestPlug = p.GetComponent<PlugController>();
                    minDistance = distance;
                }
            }
        }
    }


    void Update ()
    {
        if(nearestPlug == null)
        {
            target = 0;
        }
        if(target == 1)
        {
            if (nearestPlug.connected)
            {
                if (enemyHealth.currentHealth > 0)
                {
                    nav.SetDestination(nearestPlug.transform.position);
                }
                else
                {
                    nav.enabled = false;
                }
            }
            else
            {
                if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
                {
                    nav.SetDestination(player.position);
                }
                else
                {
                    nav.enabled = false;
                }
            }
        }
        else
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
    }
}
