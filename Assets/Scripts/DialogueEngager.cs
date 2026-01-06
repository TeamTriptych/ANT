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
    GameObject gameManager;

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
    Animator dialogueAnimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to game manager
        gameManager = GameObject.Find("GameManager");
        
        //assign ref to dialogue animator from the object group
        dialogueAnimator = dialogueObjGroup.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //collision function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //flip inDialogue to true
        gameManager.GetComponent<GameManagerBehavior>().inDialogue = true;
        //store self as the active DialogueEngager
        gameManager.GetComponent<GameManagerBehavior>().activeDialogueEngager = this;
        //show the first dialogue and text
        showDialogue(currentDialogueObjIndex);
        //show the dialogue Sprite
        dialogueSprite.GetComponent<Image>().enabled = true;
        //and call the up animation
        animateUpDialogue();
    }
    //hides the current dialogueBGObj and dialogueTextObj, increments the Index, then shows the next. Also wraps up dialogue if we are on the last Index.
    public void advanceDialogue()
    {
        
        //if the currentIndex is the final Index (Count - 1)...
        if (currentDialogueObjIndex == dialogueBGObjs.Count - 1)
        {
            //...then flip inDialogue off
            gameManager.GetComponent<GameManagerBehavior>().inDialogue = false;
            //and set activeDialogueEngager to null
            gameManager.GetComponent<GameManagerBehavior>().activeDialogueEngager = null;
            //and turn off the collision for this Obstacle
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //and hide the dialogue sprite
            dialogueSprite.GetComponent<Image>().enabled = false;
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
        //play animation that "slides" the dialogue box down behind the HUD
        dialogueAnimator.Play("DialogueBoxDown");
    }
}
