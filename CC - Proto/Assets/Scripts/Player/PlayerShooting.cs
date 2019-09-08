﻿using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                  //Bullet Damage
    public float timeBetweenBullets = 0.15f;        //Attack Speed
    public float range = 100f;                      //Gun Range

    float timer;                                    //A timer to determine when to fire
    Ray shootRay;                                   //A ray from the gun end forwards
    RaycastHit shootHit;                            //A raycast hit to get information about what was hit
    int shootableMask;                              //A layer mask for things on the shootable layer
    ParticleSystem gunParticles;                    //Particle system
    LineRenderer gunLine;                           //Line renderer
    AudioSource gunAudio;                           //Audio source
    Light gunLight;                                 //Reference to the light component
    float effectsDisplayTime = 0.2f;                //The proportion of the timeBetweenBullets that the effects will display for

    void Awake()
    {

        shootableMask = LayerMask.GetMask("Shootable"); //Create a layer mask for the Shootable layer

        gunParticles = GetComponent<ParticleSystem>();      // Set up the references
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;    // Add the time since Update was last called to the timer.

        
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)    // If the Fire1 button is being press and it's time to fire
        {
            //shoot
            Shoot();
        }

        //If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            
            DisableEffects();   //disable the effects
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;     //Reset the timer

        gunAudio.Play();  //audio play

        gunLight.enabled = true;  //activate flash 

        //Stop the particles from playing if they were, then start the particles
        gunParticles.Stop();
        gunParticles.Play();

        //Enable the line renderer and set it's first position to be the end of the gun
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        //Set the shootRay so that it starts at the end of the gun and points forward from the barrel
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //Perform the raycast against gameobjects on the shootable layer and if it hits something
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Try and find an EnemyHealth script on the gameobject hit
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            //If the EnemyHealth component exist
            if (enemyHealth != null)
            {
                //the enemy should take damage
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            //Set the second position of the line renderer to the point the raycast hit
            gunLine.SetPosition(1, shootHit.point);
        }
        //If the raycast didn't hit anything on the shootable layer
        else
        {
            //set the second position of the line renderer to the fullest extent of the gun's range
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
