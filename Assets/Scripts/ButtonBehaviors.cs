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
    }
    //this fct loads Controls Scene

    public void LoadControlsScene()
    {
        SceneManager.LoadScene("ControlsScene");
    }
    //loads Main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
    //loads intro manga scene
    public void LoadIntroMangaScene()
    {
        SceneManager.LoadScene("IntroManga");
        Debug.Log("Running LoadLevelOne, flipping isFading");
        //flip isFading to on so main menu misic will fade out
        gameManager.isFading = true;
    }
}
