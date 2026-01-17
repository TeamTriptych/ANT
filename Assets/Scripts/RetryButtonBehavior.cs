using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButtonBehavior : MonoBehaviour
{
    //ref to singleton
    GameManagerBehavior gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assing ref to singleton
        gameManager = GameManagerBehavior.singleton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        //play sfx
        gameManager.audioSources[3].Play();
        SceneManager.LoadScene("LevelOne");
    }
}
