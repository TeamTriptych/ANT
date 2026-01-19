using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // -- REFERENCES --

    //ref to the Player's Gameobj
    GameObject playerObj;

    // -- CAMERA-BEHAVIOR --

    //How far to the right of the PLayer the camera's destination is. In other words, how far left the Player will be on screen.
    public float cameraXOffset = 6.15f;
    //An abstraction of the camera's final destination, equal to the Playet's X plus the offset. Updated every frame.
    Vector3 destinationPos;

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
        //update the Camera's destination by taking the Player's x and adding the offset
        destinationPos = new Vector3(playerObj.GetComponent<Transform>().position.x + cameraXOffset, currentPos.y, currentPos.z);

        // -- FINALIZATION/ ASSIGNMENT --

        //move towards destination position
        //this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(currentPos, destinationPos, .1f);
        this.GetComponent<Transform>().position = destinationPos;
    }
}
