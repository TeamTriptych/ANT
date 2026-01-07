using UnityEngine;
using System.Collections.Generic;
public class FootBehavior : MonoBehaviour
{
    // ref to singleton
    GameManagerBehavior gameManager;
    //ref to player
    GameObject player;
    //bool to determine if this is the left foot or the right foot. true = left, false = right
    public bool isLeftFoot = true;
    //abstract of the magnitude of the offset from the top of the highest ant that the foot should have its height at
    float yOffsetFromAnt = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to singleton
        gameManager = GameManagerBehavior.singleton;
        //assign ref to player
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //abstract currentPos
        Vector3 currentPos = this.gameObject.GetComponent<RectTransform>().position;
        //every frame, if we are left
        if (isLeftFoot == true)
        {
            //abstract a ref to the leftAntsList for easier referencing
            List<GameObject> leftList = player.GetComponent<PlayerMovement>().leftAntsList;
            //temp var to store the highest ant yPos
            float highestAntYPos = 0f;
            //iterate over leftAntList, and store that ant's yPos if it's greater than the current yPos
            for (int currentIndex = 0; currentIndex < leftList.Count; currentIndex = currentIndex + 1)
            {
                //if the current ant's yPos is higher than the highestAntYPos
                if (leftList[currentIndex].GetComponent<RectTransform>().position.y > highestAntYPos)
                {
                    //store that ant's yPos as the new highest
                    highestAntYPos = leftList[currentIndex].GetComponent<RectTransform>().position.y;
                }
            }
            //finally, assign the highestAntYPos to the foot's yPos, adjusted according to the yOffset
            this.gameObject.GetComponent<RectTransform>().position = new Vector3(currentPos.x, highestAntYPos + yOffsetFromAnt, currentPos.z);
        }
        //else, we must be right
        else
        {
            //abstract a ref to the rightAntsList for easier referencing
            List<GameObject> rightList = player.GetComponent<PlayerMovement>().rightAntsList;
            //temp var to store the highest ant yPos
            float highestAntYPos = 0f;
            //iterate over leftAntList, and store that ant's yPos if it's greater than the current yPos
            for (int currentIndex = 0; currentIndex < rightList.Count; currentIndex = currentIndex + 1)
            {
                //if the current ant's yPos is higher than the highestAntYPos
                if (rightList[currentIndex].GetComponent<RectTransform>().position.y > highestAntYPos)
                {
                    //store that ant's yPos as the new highest
                    highestAntYPos = rightList[currentIndex].GetComponent<RectTransform>().position.y;
                }
            }
            //finally, assign the highestAntYPos to the foot's yPos, adjusted according to the yOffset
            this.gameObject.GetComponent<RectTransform>().position = new Vector3(currentPos.x, highestAntYPos + yOffsetFromAnt, currentPos.z);
        }
    }
}
