using UnityEngine;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
{
    //ref to singleton
    GameManagerBehavior gameManager;

    // -- MOVEMENT --

    //an abstraction of the maximum force possibly applied during each movement. This force is applied to x and y axes during movement.
    public float maxMoveForce = 3f;
    //abstraction of the degree to which all velocity is passively reduced every frame.
    public float frictionFactor = .1f;
    //the minimum possible velocity for the X Vector. Should be 0 so we never go backwards.
    float minXLinearVelocity = 0f;
    //maximum possible velocity for either vector. Y uses this value after multiplying it by -1 as its minimum, so that it functions as a max negative velocity for Y.
    public float maxLinearVelocity = 1.75f;
    //This boolean determines if movement is fixed (provides a static force factor when input is registered), or dynamic (ants provide less force the closer they are to the apex of their jump)
    public bool fixedMovement = false;

    // -- INPUT --

    //stores the last input parsed as a string
    string keyPressed;
    //public List that holds all Left Foot Ants
    public List<GameObject> leftAntsList = new List<GameObject>();
    //public List that holds all Right Foot Ants
    public List<GameObject> rightAntsList = new List<GameObject>();
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to the singleton
        gameManager = GameManagerBehavior.singleton;
    }

    // -- ANIMATION --

    //ref to player animator
    public Animator playerAnimator;
    //the lowest value of linear velocity that plays the "running" animation, not the "standing" animation
    public float lowestRunSpeed;
    //tracks if we are stationary
    public bool currentlyStationary = true;
    //tracks how fast we are going relative to how fast we could be going
    float currentProportionalVelocity = 0;
    //minimum running animation speed
    public float minAnimationSpeed = .5f;
    //maximum running animation speed
    public float maxAnimationSpeed = 1.5f;

    // -- COLLISION --

    //bool used to track if player is experiencing a bounce from a non-ped obstacle. Toggled on by said obstacle during a collision, toggled off by timer. Disables control and some passive changes like friction.
    public bool beingBounced = false;
    //the amount of time in frames that the player should be in the beingBounced state.
    public int bounceDuration = 45;
    //timer used to track if the player is still beingBounced. Toggles off the beingBounced bool when reduced to 0. should never be altered in-editor.
    public int currentBounceTimer;

    // Update is called once per frame
    void Update()
    {
        // -- POLLING INPUT --

        //check for any key
        if (Input.anyKeyDown == true)
        {
            //store key pressed
            keyPressed = Input.inputString;
            //then, if the keyPressed was space, correct it to "Space"
            if (keyPressed == " ")
            {
                keyPressed = "Space";
            }
            //Debug.Log("just pressed " + keyPressed);
        }
        //assign to null if key was released. Safe on null. Safe on more than one key press.
        if (keyPressed != null && keyPressed.Length == 1 && Input.GetKeyUp(keyPressed.ToLower()))
        {
           //Debug.Log("just released " + keyPressed);
            keyPressed = null;
        }
    }
    private void FixedUpdate()
    {
        // -- PARSING THE POLLED INPUT --

        //If we are in dialogue, parse input for dialogue
        if (gameManager.inDialogue == true)
        {
            //As long as key pressed is not null AND the active Dialogue Animator is not currently animating the dialogue...
            if (keyPressed != null && gameManager.activeDialogueEngager.currentlyAnimating == false)
            {
                //advance whatever dialogue is currently up
                gameManager.activeDialogueEngager.advanceDialogue();
                //then set the keypressed to null to prevent spam
                keyPressed = null;
            }
        }
        //else, parse input for movement as long as we are not being bounced
        else if (beingBounced == false)
        {
            //check if keyPressed matched a leftInput
            for (int currentIndex = 0; currentIndex < leftInputs.Count; currentIndex = currentIndex + 1)
            {
                if (keyPressed == leftInputs[currentIndex] || keyPressed == leftInputs[currentIndex].ToUpper())
                {
                    //Debug.Log("detected a leftFoot input");
                    //if y velocity is currently negative, clamp it to 0 min
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY = Mathf.Clamp(this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY, 0, maxLinearVelocity);
                    //trigger the jump function of the L Ant at the same Index as the input
                    leftAntsList[currentIndex].GetComponent<AntBehavior>().jump();
                }
            }
            //check if keyPressed matched a rightInput
            for (int currentIndex = 0; currentIndex < rightInputs.Count; currentIndex = currentIndex + 1)
            {
                if (keyPressed == rightInputs[currentIndex] || keyPressed == rightInputs[currentIndex].ToUpper())
                {
                    //Debug.Log("detected a rightFoot input");
                    //if y velocity is currently pos, clamp it to 0 max
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY = Mathf.Clamp(this.gameObject.GetComponent<Rigidbody2D>().linearVelocityY, maxLinearVelocity * -1, 0);
                    //trigger the jump function of the R Ant at the same Index as the input
                    rightAntsList[currentIndex].GetComponent<AntBehavior>().jump();
                }
            }
            //finally, after parsing input, set the keyPressed to null to prevent spam
            keyPressed = null;
        }

        //abstract current linearVelocity. This value is then directly modified before finally being assigned back into the actual rigidbody.
        Vector2 workingLinearVelocity = this.gameObject.GetComponent<Rigidbody2D>().linearVelocity;
        //Debug.Log("current linear velocity is " + workingLinearVelocity);

        // -- ANIMATION CHANGES --

        //update currentProportionalSpeed
        currentProportionalVelocity = workingLinearVelocity.x / maxLinearVelocity;
        //if we are moving more slowly than the lowestRunSpeed, toggle isRunning on
        if (Mathf.Abs(workingLinearVelocity.x) < lowestRunSpeed && Mathf.Abs(workingLinearVelocity.y) <= lowestRunSpeed)
        {
            currentlyStationary = true;
        }
        //If we are moving faster than lowestRunSpeed
        else
        {
            currentlyStationary = false;
        }
        if (currentlyStationary == true)
        {
            Debug.Log("stationary");
            playerAnimator.Play("Standing");
        }
        //if we are not stationary, adjust animation speed
        else
        {
            Debug.Log("moving");
            //set animation speed to between min and max, in proportion to current velocity
            playerAnimator.speed = Mathf.Lerp(minAnimationSpeed, maxAnimationSpeed, currentProportionalVelocity);
            playerAnimator.Play("Run");
        }

        // -- PASSIVE CHANGES --

        //Friction must be applied conditionally. It should be applied positively when vectors are negative, and negatively when vectors are positive. This ensures it always brings them towards 0.

        //If current yVelocity is positive...
        if (workingLinearVelocity.y > 0)
        {
           //reduce yVelocity by frictionFactor to bring it toward 0.
           workingLinearVelocity = new Vector2(workingLinearVelocity.x, workingLinearVelocity.y - frictionFactor);
        }
        //if currentYVelocity is negative...
        else if (workingLinearVelocity.y < 0)
        {
            //increase yVelocity by frictionFactor to bring it toward 0.
            workingLinearVelocity = new Vector2(workingLinearVelocity.x, workingLinearVelocity.y + frictionFactor);
        }
        //If current xVelocity is positive...
        if (workingLinearVelocity.x > 0)
        {
            //reduce xVelocity by frictionFactor to bring it toward 0.
            workingLinearVelocity = new Vector2(workingLinearVelocity.x - frictionFactor, workingLinearVelocity.y);
        }
        //if current xVelocity is negative...
        else if (workingLinearVelocity.x < 0)
        {
            //increase xVelocity by frictionFactor to bring it toward 0.
            workingLinearVelocity = new Vector2(workingLinearVelocity.x + frictionFactor, workingLinearVelocity.y);
        }
        //if we are currently being bounced
        if (beingBounced == true)
        {
            //reduce the currentBounceTImer by one each frame
            currentBounceTimer = currentBounceTimer - 1;
            //then, if it has reached 0, toggle beingBounced off
            if (currentBounceTimer == 0)
            {
                beingBounced = false;
            }
        }

        // -- FINAL CHANGES/ CORRECTIONS --

        //If we are not being bounced, clamp xVelocity between 0 and max, and yVelocity between a negative and positive max
        if (beingBounced == false)
        {
            workingLinearVelocity = new Vector2(Mathf.Clamp(workingLinearVelocity.x, minXLinearVelocity, maxLinearVelocity), Mathf.Clamp(workingLinearVelocity.y, maxLinearVelocity * -1, maxLinearVelocity));
        }
        //finally, as a double-check, if X was just clamped to 0, then that means we should be at a standstill. So, force YVelocity to 0 as well.
        if (workingLinearVelocity.x == 0)
        {
            //force yVelocity to 0
            workingLinearVelocity.y = 0;
        }
        
        // -- ASSIGNMENT TO RIGIDBODY --

        //actually assign the workingLinearVelocity back to the Rigidbody2D
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = workingLinearVelocity;
        Debug.Log("current linearVelocity is " + workingLinearVelocity);

    }
}
