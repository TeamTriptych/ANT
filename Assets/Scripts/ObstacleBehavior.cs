using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    //how much force is applied to the Player on collision
    public float pushbackForce = 50;
    // the game object that will appear when anny collides with an obstacle : ) : ) : )
    public GameObject comicBubble;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this fct runs when a player collider enters this collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //instantiate an "ow!" comic bubble
        Instantiate(comicBubble, collision.gameObject.GetComponent<Transform>().position, Quaternion.identity );
        //probably play a sound here or something idk yet
        
        //set player's velocity to 0
        collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        //push them back
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(pushbackForce * -1, 0, 0));
        //toggle their beingBounced boolean
        collision.gameObject.GetComponent<PlayerMovement>().beingBounced = true;
        //and set their bounceTimer equal to their bounceDuration
        collision.gameObject.GetComponent<PlayerMovement>().currentBounceTimer = collision.gameObject.GetComponent<PlayerMovement>().bounceDuration;
        
        //turn off this object's collider

        //attempt to find a BoxCollider
        if (this.gameObject.GetComponent<BoxCollider2D>() != null)
        {
            //turn off the collider
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        //else it must have a Polygon Collider
        else
        {
            //turn off the collider
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
