using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // -- REFERENCES --

    //ref to the Player's Gameobj
    GameObject playerObj;

    // -- CAMERA-BEHAVIOR --

    //establishes the hard limit on how far to the right the camera will travel. Prevents the camera from following anny into school.
    public float maxXPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref
        playerObj = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //abstract currentPos for easy referencing
        Vector3 currentPos = this.gameObject.GetComponent<Transform>().position;
        //if the camera's current x pos is beyond the maxXPos, set it to the maxXPos
        if (this.gameObject.GetComponent<Transform>().position.x > maxXPos) 
        {
            Debug.Log("attempting to correct camera XPos");
            //set x Pos to maxXPos
            this.gameObject.GetComponent<Transform>().position = new Vector3(maxXPos, currentPos.y, currentPos.z);
        }
    }
}
