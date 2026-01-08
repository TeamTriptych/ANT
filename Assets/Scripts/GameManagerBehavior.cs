using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    /*stores the instance of the first GameManagerBehavior script created. It is then used as a safety check to ensure any duplicate GameManager objects delete themselves.
     * Must start as null. Made static so that its reference persists across different instances of GameManagerBehavior scripts. */
    public static GameManagerBehavior singleton = null;

    // -- TIME-RELATED --

    //public variable that tracks frames since game start
    public int frameCounter = 0;
    //public var that tracks total time elapsed while in level. Assigned by TimerBehavior during level. Used to display final time during Outro
    public string finalTime;
    //the second at which toast integrity becomes %0.
    public int targetTimeInSeconds = 180;
    //public variable that stores the "integrity" of the toast as a percent. Assigned from TimerBehavior, in relation to targetSeconds variable.
    public float toastIntegrity;

    // -- DIALOGUE-RELATED --

    //bool to track if we are currently in dialogue. Flipped on by DialogueEngager; off by advanceDialogue() in DialogueEngager
    public bool inDialogue = false;
    //ref to the active Dialogue Engager. Set when colliding with an Obstacle. Unset by advanceDialogue() in DialogueEngager.
    public DialogueEngager activeDialogueEngager = null;

    //called when the script is loaded
    private void Awake()
    {
        //If a GameManagerBehavior instance has not been marked as the singleton, mark this instance as the singleton
        if (singleton == null)
        {
            singleton = this;
        }
        //else if there is already a singleton stored, and it is NOT this instance...
        else if (singleton != this)
        {
            //destroy this entire Game Manager object
            Destroy(this.gameObject);
        }
        //Finally, if we have made it this far without being destroyed, then this object carries our singleton. Mark it as Don't Destroy.
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        //update the frameCounter
        frameCounter = frameCounter + 1;
    }
}
