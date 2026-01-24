using TMPro;
using UnityEngine;

public class FinalTimeBehavior : MonoBehaviour
{
    //ref to singleton
    GameManagerBehavior gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to singleton
        gameManager = GameManagerBehavior.singleton;
        //on start, display the finalTime logged in the gameManager
        this.gameObject.GetComponent<TextMeshPro>().text = "FINAL TIME: " + gameManager.finalTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
