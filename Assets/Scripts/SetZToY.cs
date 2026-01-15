using UnityEngine;

public class SetZToY: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //abstract currentPos for easy referencing
        Vector3 currentPos = this.gameObject.GetComponent<Transform>().position;
        //assign Z pos equal to y pos
        this.gameObject.GetComponent<Transform>().position = new Vector3(currentPos.x, currentPos.y, currentPos.y);
    }
}
