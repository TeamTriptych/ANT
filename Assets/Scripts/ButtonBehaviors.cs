using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviors : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
    }
}
