using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviors : MonoBehaviour
{
    //ref to singleton
    GameManagerBehavior gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to singleton
        gameManager = GameManagerBehavior.singleton;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //loads Level One
    public void LoadLevelOne()
    {
        SceneManager.LoadScene("LevelOne");
        //play sfx
        gameManager.audioSources[3].Play();
    }
    //this fct loads Controls Scene

    public void LoadControlsScene()
    {
        //play sfx
        gameManager.audioSources[1].Play();
        SceneManager.LoadScene("ControlsScene");
    }
    //loads Main Menu
    public void LoadMainMenu()
    {
        //play sfx
        gameManager.audioSources[4].Play();
        SceneManager.LoadScene("MainMenu");

    }
    //loads intro manga scene
    public void LoadIntroMangaScene()
    {
        //play sfx
        gameManager.audioSources[2].Play();
        SceneManager.LoadScene("IntroManga");
        //Debug.Log("Running LoadLevelOne, flipping isFading");
        //flip isFading to on so main menu misic will fade out
        gameManager.isFading = true;
    }
}
