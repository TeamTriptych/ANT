using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTriggerBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //runs when another collider touches this one
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the other collider belonged to the player...
        if (collision.gameObject.name == "Player")
        {
            //load the Outro Manga Scene
            SceneManager.LoadScene("OutroManga");
        }
    }
}
