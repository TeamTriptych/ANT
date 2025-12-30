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
        
    }
    void FixedUpdate()
    {
        //update the frameCounter
        frameCounter = frameCounter + 1;
    }
}
