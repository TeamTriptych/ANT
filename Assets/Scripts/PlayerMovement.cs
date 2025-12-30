using UnityEngine;
using System.Collections.Generic;
public class PlayerMovement : MonoBehaviour
{
    //an abstraction of the maximum force possibly applied during each movement. This force is applied to x and y axes during movement.
    public float maxMoveForce = 3f;
    //abstraction of the degree to which all velocity is passively reduced every frame.
    public float frictionFactor = .1f;
    //the minimum possible velocity for the X Vector. Should be 0 so we never go backwards.
    float minXLinearVelocity = 0f;
    //maximum possible velocity for either vector. Y uses this value after multiplying it by -1 as its minimum, so that it functions as a max negative velocity for Y.
    float maxLinearVelocity = 1f;
    //ref to game manager
    GameObject gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to game manager
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        // -- INPUT --

        //check for any key
        if (Input.anyKeyDown == true)
        {
            //list of all possible inputs that should trigger the left foot
            List<string> leftInputs = new List<string>()
            {
                "1", "2", "3", "4", "5", "6", "q", "w", "e", "r", "t", "y", "a", "s", "d", "f", "g", "h", "z", "x", "c", "v", "b"
            };
            //list of all possible inputs that should trigger the right foot
            List<string> rightInputs = new List<string>()
            {
                "7", "8", "9", "0", "-", "=", "u", "i", "o", "p", "[", "]", "j", "k", "l", ";", "'", "n", "m", ",", ".", "/"
            };
            //check key pressed
            string keyPressed = Input.inputString;
            Debug.Log("just pressed " + keyPressed);
            //check if keyPressed matched a leftInput
            for (int currentIndex = 0; currentIndex < leftInputs.Count; currentIndex = currentIndex + 1)
            {
                if (keyPressed == leftInputs[currentIndex])
                {
                    Debug.Log("detected a leftFoot input");
                    //if y velocity is currently negative, clamp it to 0 min
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY = Mathf.Clamp(this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY, 0, maxLinearVelocity);
                    //trigger the jump function of the L Ant at the same Index as the input
                    gameManager.GetComponent<GameManagerBehavior>().leftAntsList[currentIndex].GetComponent<AntBehavior>().jump();
                }
            }
            //check if keyPressed matched a rightInput
            for (int currentIndex = 0; currentIndex < rightInputs.Count; currentIndex = currentIndex + 1)
            {
                if (keyPressed == rightInputs[currentIndex])
                {
                    Debug.Log("detected a rightFoot input");
                    //if y velocity is currently pos, clamp it to 0 max
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY = Mathf.Clamp(this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY, maxLinearVelocity * -1, 0);
                    //trigger the jump function of the R Ant at the same Index as the input
                    gameManager.GetComponent<GameManagerBehavior>().rightAntsList[currentIndex].GetComponent<AntBehavior>().jump();
                }
            }
        }
    }
    private void FixedUpdate()
    {
        //abstract current linearVelocity. This value is then directly modified before finally being assigned back into the actual rigidbody.
        Vector2 workingLinearVelocity = this.gameObject.GetComponent<Rigidbody2D>().linearVelocity;
        Debug.Log("current linear velocity is " + workingLinearVelocity);

        // -- PASSIVE CHANGES --

        //Friction must be applied differently to X and Y axes. X is always reduced by friction Factor, but Y must be increased or decreased contextually depending on movement direction.
        //If current yVelocity is positive...
        if (workingLinearVelocity.y > 0)
        {
           //reduce yVelocity by frictionFactor to bring it toward 0.
           workingLinearVelocity = new Vector2(workingLinearVelocity.x - frictionFactor, workingLinearVelocity.y - frictionFactor);
        }
        //if currentyVelocity is negative...
        else if (workingLinearVelocity.y < 0)
        {
            //increase yVelocity by frictionFactor to bring it toward 0.
            workingLinearVelocity = new Vector2(workingLinearVelocity.x - frictionFactor, workingLinearVelocity.y + frictionFactor);
        }

        // -- FINAL CHANGES/ CORRECTIONS --

        //clamp xVelocity between 0 and max, and yVelocity between a negative and positive max
        workingLinearVelocity = new Vector2(Mathf.Clamp(workingLinearVelocity.x, minXLinearVelocity, maxLinearVelocity), Mathf.Clamp(workingLinearVelocity.y, maxLinearVelocity * -1, maxLinearVelocity));
        //finally, as a double-check, if X was just clamped to 0, then that means we should be at a standstill. So, force YVelocity to 0 as well.
        if (workingLinearVelocity.x == 0)
        {
            //force yVelocity to 0
            workingLinearVelocity.y = 0;
        }

        // -- ASSIGNMENT TO RIGIDBODY --

        //actually assign the workingLinearVelocity back to the Rigidbody2D
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = workingLinearVelocity;
        //Debug.Log("current linearVelocity is " + workingLinearVelocity);
    }
}
