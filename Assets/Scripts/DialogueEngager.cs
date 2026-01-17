using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEngager : MonoBehaviour
{
    /* THIS SCRIPT IS RESPONSIBLE FOR ENGAGING WITH THE SPECIFIC DIAOGUES RELEVENT TO THE OBSTACLE IT IS ATTACHED TO.
     * IT HOOKS ITSELF UP TO THE REST OF THE GAME THROUGH THE GAME MANAGER AFTER A COLLISION */

    // -- REFERENCES --

    //ref to game manager
    GameManagerBehavior gameManager;

    // -- DIALOGUE --

    //ref to this Obstacle's Dialogue Group, which is the Parent Obj that holds all relevent Dialogue. used for animating.
    public GameObject dialogueObjGroup;
    //list of all UI DialogueBGs that should be triggered. assigned in-editor.
    public List<GameObject> dialogueBGObjs = new List<GameObject>();
    //list of all UI DialogueBGFrames that should be triggered. assigned in-editor.
    public List<GameObject> dialogueBGFrameObjs = new List<GameObject>();
    //list of all the UI Dialogue Text Objs that should be triggered. assigned in-editor.
    public List<GameObject> dialogueTextObjs = new List<GameObject>();
    //public ref to the Dialogue Sprite for this Obstacle. Shown during collision, hidden during final instance of advanceDialogue().
    public GameObject dialogueSprite;
    //acts as a public Index for iterating over the DialogueObjs List. Starts at 0, reset by advanceDialogue.
    int currentDialogueObjIndex = 0;

    // -- ANIMATION-RELATED --

    //ref to Animator component on the dialogue Obj
    Animator dialogueAnimator;
    //boolean that tracks if the dialoguebox is animating. Toggled on by animateUpDialogue(). Toggled off after animationTime reaches 0.
    public bool currentlyAnimating = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to game manager
        gameManager = GameManagerBehavior.singleton;
        
        //assign ref to dialogue animator from the object group
        dialogueAnimator = dialogueObjGroup.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    //collision function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //flip inDialogue to true
        gameManager.inDialogue = true;
        //store self as the active DialogueEngager
        gameManager.activeDialogueEngager = this;
        //show the first dialogue and text
        showDialogue(currentDialogueObjIndex);
        //show the dialogue Sprite
        dialogueSprite.GetComponent<Image>().enabled = true;
        //and call the up animation
        animateUpDialogue();
        //set the player's linear velocity to 0
        collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
    }
    //hides the current dialogueBGObj and dialogueTextObj, increments the Index, then shows the next. Also wraps up dialogue if we are on the last Index.
    public void advanceDialogue()
    {
        
        //if the currentIndex is the final Index (Count - 1)...
        if (currentDialogueObjIndex == dialogueBGObjs.Count - 1)
        {
            //and animate the dialogue box off the screen
            animateDownDialogue();
        }
        //else, increment and show the next dialogue
        else
        {
            //hide text, frame, and BG for current Dialogue
            hideDialogue(currentDialogueObjIndex);
            //increment the dialogueObj Index
            currentDialogueObjIndex = currentDialogueObjIndex + 1;
            //show the text and BG for "next" dialogue
            showDialogue(currentDialogueObjIndex);
        }

    }
    //"Shows" the dialogueBGObj and dialogueTextObj at the given index from their Lists. Right now this means activating their Renderer Components.
    void showDialogue(int dialogueIndex)
    {
        //show the current dialogue BG 
        dialogueBGObjs[currentDialogueObjIndex].GetComponent<Image>().enabled = true;
        //show the current dialogue BG Frame
        dialogueBGFrameObjs[currentDialogueObjIndex].GetComponent<Image>().enabled = true;
        //show the current dialogue Text
        dialogueTextObjs[currentDialogueObjIndex].GetComponent<TextMeshProUGUI>().enabled = true;
    }
    //"Hides" the dialogueBGObj and DialogueTextObj at the given index from their Lists. Right now this means de-activating their Renderer Components.
    void hideDialogue(int dialogueIndex)
    {
        //hide the current dialogue BG
        dialogueBGObjs[currentDialogueObjIndex].GetComponent<Image>().enabled = false;
        //hide the current dialogue BG Frame
        dialogueBGFrameObjs[currentDialogueObjIndex].GetComponent<Image>().enabled = false;
        //hide the current dialogue Text
        dialogueTextObjs[currentDialogueObjIndex].GetComponent<TextMeshProUGUI>().enabled = false;
    }
    /* Handles moving all relevant dialogue boxes on to the screen as a group. Only called one time, on first collision. This ensures that the animation effects all dialogues
     * regardless of which is actually being displayed, allowing the player to mash through text during the animation. We could also flip a bool here if we don't want to allow mashing
     * until the animation completes.*/

    public void animateUpDialogue()
    {
        //play animation that "slides" the dialogue box up from behind the HUD
        dialogueAnimator.Play("DialogueBoxUp");
    }
    /* Handles moving all relevant dialogue boxes below off the screen as a group. Only called one time, after the last call to advanceDialogue(). This ensures that the animation effects all dialogues
     * regardless of which is actually being displayed, allowing the player to mash through text during the animation. We could also flip a bool here if we don't want to allow mashing
     * until the animation completes.*/
    public void animateDownDialogue()
    {
        //First, start animation that "slides" the dialogue box down behind the HUD
        dialogueAnimator.Play("DialogueBoxDown");
    }
    //This fct checks if the Animator is currently in any "animation" states, and updates states accordingly. Does nothing if it finds an unrelated state.
    public void updateAnimationStates()
    {
        //check if the current state is animating up
        if (dialogueAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBoxUp"))
        {
            //If it is, check if it has completed a loop yet. Use normalized time, as normalized time returns a completion percentage (less than one is not completed, more than one means we are additional frames past a loop)
            if (dialogueAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                //if less than one, we haven't finished a loop yet. make sure currentlyAnimating is true.
                currentlyAnimating = true;
            }
            else
            {
                //If greater than one, we've completed at least one loop, so we must be done. make sure currentlyAnimating is false.
                currentlyAnimating = false;
            }
        }
        //if the state is animating down
        if (dialogueAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBoxDown"))
        {
            //If it is, check if it has finished.

            //If it has started but hasn't finished, toggle currentlyAnimating on to lock input and prevent trying to advance dialogue while the animation plays
            if (dialogueAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 && dialogueAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                currentlyAnimating = true;
            }
            //else if the animation has finished, toggle currentlyAnimating off and wrap up dialogue.
            else if (dialogueAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                currentlyAnimating = false;
                //...then flip inDialogue off
                gameManager.inDialogue = false;
                //and set activeDialogueEngager to null
                gameManager.activeDialogueEngager = null;
                //and turn off the collision for this Obstacle
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                //and hide the dialogue sprite
                dialogueSprite.GetComponent<Image>().enabled = false;
            }
            
        }
    }
}
