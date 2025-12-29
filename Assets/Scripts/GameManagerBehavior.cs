using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    //public variable that tracks frames since game start
    public int frameCounter = 0;
    //public List that holds all Left Foot Ants
    public List<GameObject> leftAntsList = new List<GameObject>();
    //public List that holds all Right Foot Ants
    public List<GameObject> rightAntsList = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
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
                "7", "&", "8", "*", "9", "(", "0", ")", "-", "_", "=", "+", "U", "u", "I", "i", "O", "o", "P", "p", "{", "[", "}", "J", "j", "K", "k", "L", "l", ":", ";", "\"", "'", "N", "n", "M", "m", "<", ",", ">", ".", "?", "/"
            };
            //check key pressed
            string keyPressed = Input.inputString;
            Debug.Log("just pressed " + keyPressed);
            //check if keyPressed matched a leftInput
            for (int currentIndex = 0; currentIndex < leftInputs.Count; currentIndex = currentIndex + 1)
            {
                if (keyPressed == leftInputs[currentIndex])
                {
                    Debug.Log("GM detected a leftFoot input");
                    //trigger the jump function of the L Ant at the same Index as the input
                    leftAntsList[currentIndex].GetComponent<AntBehavior>().jump();
                }
            }
            //check if keyPressed matched a rightInput
            for (int currentIndex = 0; currentIndex < rightInputs.Count; currentIndex = currentIndex + 1)
            {
                if (keyPressed == rightInputs[currentIndex])
                {
                    Debug.Log("GM detected a rightFoot input");
                    
                }
            }
        }
    }
    void FixedUpdate()
    {
        //update the frameCounter
        frameCounter = frameCounter + 1;
    }
}
