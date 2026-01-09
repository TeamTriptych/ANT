using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ToastBehavior : MonoBehaviour
{
    //ref to singleton
    GameManagerBehavior gameManager;
    //sprites for toast states. 0 is full integrity, 5 is none.
    public Sprite toast0;
    public Sprite toast1;
    public Sprite toast2;
    public Sprite toast3;
    public Sprite toast4;
    public Sprite toast5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to singleton
        gameManager = GameManagerBehavior.singleton;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*First, adjust behavior according to if this Toast is a UI or in-world object.
        If it is UI, it will have an Image Component. If World Object, it will have a 
        Sprite Renderer Component. */

        //If it DOES have an Image component...
        if (this.gameObject.GetComponent<Image>() != null)
        {
            //ref to this Image Component
            Image imageComponent = this.gameObject.GetComponent<Image>();
            //adjust the Image Component sprite according to toastIntegrity
            if (gameManager.toastIntegrity <= 100 && gameManager.toastIntegrity >= 91)
            {
                //between 100 and 91, full
                imageComponent.sprite = toast0;
            }
            else if (gameManager.toastIntegrity <= 90 && gameManager.toastIntegrity >= 70)
            {
                //less than 90 and greater than 70, 1 integrity
                imageComponent.sprite = toast1;
            }
            else if (gameManager.toastIntegrity <= 69 && gameManager.toastIntegrity >= 50)
            {
                //between 69 and 50, 2 integrity
                imageComponent.sprite = toast2;
            }
            else if (gameManager.toastIntegrity <= 49 && gameManager.toastIntegrity >= 30)
            {
                //between 49 and 30, 3 integrity
                imageComponent.sprite = toast3;
            }
            else if (gameManager.toastIntegrity <= 29 && gameManager.toastIntegrity >= 10)
            {
                //between 29 and 10, 4 integrity
                imageComponent.sprite = toast4;
            }
            else if (gameManager.toastIntegrity <= 9)
            {
                //less than 9, 5 integrity
                imageComponent.sprite = toast5;
            }
        }
        //If it does NOT have an Image component, it must have a Sprite Renderer
        else
        {
            //ref to the Sprite Renderer Component
            SpriteRenderer spriteRendererComponent = this.gameObject.GetComponent<SpriteRenderer>();
            //adjust the Sprite Renderer sprite according to toastIntegrity
            if (gameManager.toastIntegrity <= 100 && gameManager.toastIntegrity >= 91)
            {
                //between 100 and 91, full
                spriteRendererComponent.sprite = toast0;
            }
            else if (gameManager.toastIntegrity <= 90 && gameManager.toastIntegrity >= 70)
            {
                //less than 90 and greater than 70, 1 integrity
                spriteRendererComponent.sprite = toast1;
            }
            else if (gameManager.toastIntegrity <= 69 && gameManager.toastIntegrity >= 50)
            {
                //between 69 and 50, 2 integrity
                spriteRendererComponent.sprite = toast2;
            }
            else if (gameManager.toastIntegrity <= 49 && gameManager.toastIntegrity >= 30)
            {
                //between 49 and 30, 3 integrity
                spriteRendererComponent.sprite = toast3;
            }
            else if (gameManager.toastIntegrity <= 29 && gameManager.toastIntegrity >= 10)
            {
                //between 29 and 10, 4 integrity
                spriteRendererComponent.sprite = toast4;
            }
            else if (gameManager.toastIntegrity <= 9)
            {
                //less than 9, 5 integrity
                spriteRendererComponent.sprite = toast5;
            }
        }
    }

}
