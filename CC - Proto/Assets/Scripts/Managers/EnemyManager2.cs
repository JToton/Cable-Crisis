using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager2 : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    EnemyHealth enemyHealth;          //reference the other script


    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    public int totalEnemyAmount = 10;
    public int enemyAmount = 0;

    private void Awake()
    {
        enemyHealth = GetComponentInChildren<EnemyHealth>();
    }

    void Start()
    {
        Invoke("spawnEnemiesTwo",20f);
    }

    void spawnEnemiesTwo()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Update()
    {

        if (enemyAmount == totalEnemyAmount)
        {
            CancelInvoke();
        }
        /*
        if (enemyHealth.isDead)
        {
            enemyAmount -= 1;
        }
        */

    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        enemyAmount += 1;
    }
}
