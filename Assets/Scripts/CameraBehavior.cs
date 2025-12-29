using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float cameraXOffset = 6.15f;
    //ref to the Player's Gameobj
    GameObject playerObj;
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
        //set the camera's X equal to the Player's x, plus an offset
        this.gameObject.GetComponent<Transform>().position = new Vector3(playerObj.GetComponent<Transform>().position.x + cameraXOffset, currentPos.y, currentPos.z);
    }
}
