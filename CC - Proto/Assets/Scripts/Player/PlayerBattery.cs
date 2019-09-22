using UnityEngine;
using UnityEngine.UI;           //needed to access the slider health bar

public class PlayerBattery : MonoBehaviour
{
    public int maximumBatteryCap = 100;     //max capacity
    public int minimumBatteryCap = 0;
    public int startingBattery = 100;       //starting health when the level starts
    public float currentBattery;              //hp current
    public Slider BatterySlider;            //slider UI element
    public AudioClip deadBatteryClip;      //death audio clip (Dead Battery)
    public int batteryDrain = 10;
    public float batteryGain = 10f;


    Animator animate;                  //animator components
    AudioSource playerAudio;            //audio source reference
    PlayerShooting playerShooting;
    PlayerMovement playerMovement;

    bool isDead;  //is the battery dead ?
    //public bool isCharged;
    //bool batteryInRange = false;
    //bool shooting; //shooting?


    void Awake()
    {
        animate = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        currentBattery = startingBattery;   //start of game here, set current max hp
    }


    void Update()
    {
        //if we are damaged set this image to red with 10% opac
        if (playerShooting.isFiring == true)
        {
            LoseBattery(batteryDrain);
        }
        else if (playerMovement.batteryInRange == true)
        {
            GainBattery(batteryGain);
        }
        if (playerMovement.batteryInRange)
        {
            playerShooting.isConnected = true;
        }
        else
        {
            playerShooting.isConnected = false;
        }
        //reset
        playerShooting.isFiring = false;

        playerMovement.batteryInRange = false;


    }

    //public, other scripts can call this script.
    public void LoseBattery(int amount)
    {
        //shooting = true;
        if(currentBattery > minimumBatteryCap)
        {
            currentBattery -= amount;                //reduce bat
            BatterySlider.value = currentBattery;     //pass value into slider component 
        }

        //playerAudio.Play();                    //play bat use audio

        else if (currentBattery <= 0)       //check if bat dead
        {

            DeadBattery();                           //call death function
        }
    }

    public void GainBattery(float amount)
    {
        if(currentBattery < maximumBatteryCap)
        {

            currentBattery += amount;                       //reduce bat
            BatterySlider.value = currentBattery;           //pass value into slider component 
            playerShooting.enabled = true;                  //turn on player shooting
        }
        

        //playerAudio.Play();                               //play bat use audio
       
        if (currentBattery == maximumBatteryCap)                      
        {
            ChargedBattery();                           
        }
    }

    /*
    public void Shooting(bool firing)
    {
        if (playerShooting.enabled = true)
        {
            firing = true;
        }
    }
    */

    void DeadBattery()
    {
        isDead = true;                              //Battery is dead
        playerShooting.DisableEffects();
        playerShooting.enabled = false;             //turn off player shooting

        //animate.SetTrigger("Die");                //play death animation  (Change to a dead bat clip eventually)
        //playerAudio.clip = deadBatteryClip;       //play audio death clip (for dead bat sound)
        //playerAudio.Play();
        //playerAudio.PlayDelayed(44);
    }

    void ChargedBattery()
    {
        //isCharged = true;                           //Battery is charged
        //playerShooting.enabled = true;              //turn on player shooting
    }
}
