using TMPro;
using UnityEngine;

public class FinalToastIntegrityBehavior : MonoBehaviour
{
    //ref to the singleton
    GameManagerBehavior gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to singleton
        gameManager = GameManagerBehavior.singleton;
        //display text 
        this.gameObject.GetComponent<TextMeshPro>().text = "FINAL TOAST INTEGRITY: \n" + gameManager.toastIntegrity + "%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
