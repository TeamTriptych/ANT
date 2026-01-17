using UnityEngine;
using UnityEngine.SceneManagement;

public class GoButtonBehavior : MonoBehaviour
{
    //ref to the singleton
    GameManagerBehavior gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign the singleton
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
