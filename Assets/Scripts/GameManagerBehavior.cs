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
    public float toastIntegrity = 0;

    // -- DIALOGUE-RELATED --

    //bool to track if we are currently in dialogue. Flipped on by colliding with a DialogueEngager; off once the DialogueBoxDown animation is confirmed finished by up in DialogueEngager
    public bool inDialogue = false;
    //ref to the active Dialogue Engager. Set when colliding with an Obstacle. Unset by advanceDialogue() in DialogueEngager.
    public DialogueEngager activeDialogueEngager = null;

    // -- AUDIO-RELATED --

    //public bool for tracking if the fadeTimer is active. flippped by
    public bool isFading = false;
    //time in frames that it will take for the audio volume to reach 0.
    public int desiredFadeTime = 60;
    //tracks how far we are into the fade. Set to desiredFadeTime during Start()
    public int currentFadeTime;
    //array of all AudioSources on this instance. assigned during Start()
    public AudioSource[] audioSources;

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
        //Set currentFadeTime to desiredFadeTime
        currentFadeTime = desiredFadeTime;
        //and assign the array of AudioSources
        audioSources = this.gameObject.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        // -- DIALOGUE --

        //as long as there is an activeDialogueEngager, make sure it is updating its state so we know if we're in dialogue, animating dialogue, etc
        if (activeDialogueEngager != null)
        {
            activeDialogueEngager.updateAnimationStates();
        }

        //update the frameCounter
        frameCounter = frameCounter + 1;

        // -- AUDIO --

        //as long as isFading is true
        if (isFading == true)
        {
            Debug.Log("attempting to fade");
            //first check if the currentFadeTime was previoiusly reduced to 0
            if (currentFadeTime == 0)
            {
                //flip isFading so that we don't continue this process
                isFading = false;
                //and stop the audio clip
                this.gameObject.GetComponent<AudioSource>().Stop();
            }
            else
            {
                //set currentVolume to currentFadeTime divided by desiredFadeTime (proportion of how much left of the fade we have to complete)
                this.gameObject.GetComponent<AudioSource>().volume = currentFadeTime / desiredFadeTime;
                //then subtract from currentFadeTime
                currentFadeTime = currentFadeTime - 1;
            }
        }           
    }
}
