using UnityEngine;
using UnityEngine.UI;           //needed to access the slider health bar
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; //starting health when the level starts
    public int currentHealth;        //hp current
    public Slider healthSlider;      //slider UI element
    public Image damageImage;        //damage image to flash screen
    public AudioClip deathClip;      //death audio clip (loses the game)
    public float flashSpeed = 5f;    //how quick the image flash is for damage
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); //flash mostly transparent and red


    Animator animate;                  //animator components
    AudioSource playerAudio;            //audio source reference
    PlayerMovement playerMovement;      //reference to another script - movement
    PlayerShooting playerShooting;          
    bool isDead;  //dead ?
    bool damaged; //damaged?


    void Awake ()
    {
        animate = GetComponent <Animator> ();           
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();              //get component to reference 
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;   //start of game here, set current max hp
    }


    void Update ()
    {
        //if we are damaged set this image to red with 10% opac
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        //else fade the image back to transparent
        else
        {
            //smoothing from color to transparent
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        //reset
        damaged = false;
    }

    //public, other scripts can call this script.
    public void TakeDamage (int amount)
    {
        damaged = true;
        
        currentHealth -= amount;                //reduce health

        healthSlider.value = currentHealth;     //pass value into slider component 

        playerAudio.Play ();                    //play hurt audio

        if(currentHealth <= 0 && !isDead)       //check if dead
        {
            Death ();                           //call death function
        }
    }


    void Death ()
    {
        isDead = true;                      //player is dead

        playerShooting.DisableEffects ();

        animate.SetTrigger ("Die");         //play death animation

        playerAudio.clip = deathClip;       //play audio death clip
        playerAudio.Play ();

        playerMovement.enabled = false;     //turn off player movement
        playerShooting.enabled = false;     //turn off player shooting
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
