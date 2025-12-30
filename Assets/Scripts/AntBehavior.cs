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
    //tracks if this is a leftAnt
    public bool isLeft = true;
    //ref to Player
    GameObject playerObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to Player
        playerObj = GameObject.Find("Player");
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
        //calculate the difference between destHeight and where we're starting the jump from
        float currentHeightDif = destHeight - this.gameObject.GetComponent<RectTransform>().position.y;
        //and abstract the maximum potential difference
        float maxHeightDif = destHeight - startingPos.y;
        //abstract a proportion of how close currentHeightDif is to maxHeightDif, putting it in terms between 0 and 1
        float heightProportion = currentHeightDif / maxHeightDif;
        //then lerp that proportion between 0 and maxMoveForce to translate it into a proportional magnitude of movement
        float proportionalForce = Mathf.Lerp(0, playerObj.gameObject.GetComponent<PlayerMovement>().maxMoveForce, heightProportion);
        //finally, if we an L Ant...
        if (isLeft == true)
        {
            //add the proportional force to the Player, with pos x and pos y
            playerObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(proportionalForce, proportionalForce));
        }
        //else if we are an R Ant...
        else
        {
            //add the proportional force to the player, with pos x and neg y
            playerObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(proportionalForce, (proportionalForce * -1)));
        }
    }
}
