using UnityEngine;

public class AntBehavior : MonoBehaviour
{
    /* THIS SCRIPT IS RESPONSIBLE FOR ANY BEHAVIOR SHARED BETWEEN ALL ANTS */

    //the maximum height that an Ant can jump from its starting position
    float jumpOffset = 15f;
    //the speed at which the Ant moves up and down
    float yVelocity = 1f;
    //ref to the Ant's starting position. Set during Start()
    Vector3 startingPos;
    //abstraction of the destination height for the ant to jump to. Calculated during Start()
    float destHeight;
    //tracks if the Ant should be ascending to destHeight
    bool isJumping = false;
    //tracks if the Ant should be descending to startingPos.y
    bool isFalling = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //store starting pos
        startingPos = this.gameObject.GetComponent<RectTransform>().position;
        //calculate destination height
        destHeight = startingPos.y + jumpOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //abstract currentPos
        Vector3 currentPos = this.gameObject.GetComponent<RectTransform>().position;

        // -- CHANGE HEIGHT --

        //if isJumping is true and we are less than destHeight, we should increase our y according to yVelocity
        if (isJumping == true && currentPos.y < destHeight)
        {
            //increase currentPos y
            currentPos = new Vector3(currentPos.x, currentPos.y + yVelocity, currentPos.z);
        }
        //if isFalling is true and we are higher than startingPos.y, we should decrease our y according to yVelocity
        if (isFalling == true && currentPos.y > startingPos.y)
        {
            //decrease currentPos y
            currentPos = new Vector3(currentPos.x, currentPos.y - yVelocity, currentPos.z);
        }

        // -- MAX/MIN HEIGHT CHECKS --

        //If we are at or over destHeight, flip isJumping off and isFalling on
        if (currentPos.y >= destHeight)
        {
            isJumping = false;
            isFalling = true;
        }
        //If we are at or under startingPos.y, flip isJumping off and isFalling off
        if (currentPos.y <= startingPos.y)
        {
            isJumping = false;
            isFalling = false;
        }

        // -- CLAMP HEIGHT --

        //If we are below startingPos.y or above destHeight, clamp our y to those values
        currentPos = new Vector3(currentPos.x, Mathf.Clamp(currentPos.y, startingPos.y, destHeight), currentPos.z);

        // -- ASSIGN POSITION TO TRANSFORM --

        //then assign currentPos to the RectTransform
        this.gameObject.GetComponent<RectTransform>().position = currentPos;
    }
    public void jump()
    {
        //toggle isFalling to false in case it's on
        isFalling = false;
        //toggle isJumping on so we ascend to destHeight
        isJumping = true;
        //on jump, we should flip an isJumping bool
        //start moving from whatever height we're at to the destHeight
        //then, when we reach destHeight, we should flip a bool so we start descending
        //then, we should descend until we reach startingPos.y
    }
}
