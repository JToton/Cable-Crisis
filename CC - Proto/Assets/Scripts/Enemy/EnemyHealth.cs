using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;    //starting health
    public int currentHealth;           //current health            
    public float sinkSpeed = 2.5f;      //speed to sink into the floor
    public int scoreValue = 1;         //score value increase
    public AudioClip deathClip;         


    Animator animate;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;        //prefab child object
    CapsuleCollider capsuleCollider;    //colider
    EnemyManager enemyManager;          //reference the other script
    public bool isDead;
    bool isSinking;


    void Awake ()
    {
        animate = GetComponent <Animator> ();                       //get component
        enemyAudio = GetComponent <AudioSource> ();                 //
        hitParticles = GetComponentInChildren <ParticleSystem> ();  //get children and return particle system
        capsuleCollider = GetComponent <CapsuleCollider> ();
        enemyManager = GetComponentInChildren<EnemyManager>();

        currentHealth = startingHealth;  //set health
    }


    void Update ()
    {
        if(isSinking)  //check if sinking
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);  //move the body into the floor (sink) 
        }
    }

    
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;  //if enemy is dead return out 

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        animate.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;  //turn off just the nav not the whole object
        GetComponent <Rigidbody> ().isKinematic = true;  //move a collider and unity recalcs static geo's -- not if kinematic
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);  //destroy after 2 seconds -- give it time to sink into the floor
    }
}
