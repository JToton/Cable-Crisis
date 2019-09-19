using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float timer = 5f;

    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            //Scene scene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene("scene");
            //SceneManager.LoadScene(GetActiveScene().name);


            Invoke("MyLoadingFunction", timer);
        }
    }

    void currentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("currnetScene");
    }

    void MyLoadingFunction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
