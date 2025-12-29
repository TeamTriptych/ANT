using TMPro;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{
    //ref to the GameManager
    GameObject gameManager;
    //variable for tracking total amount of seconds elapsed since game start. Incremented using gameManager's frameCounter
    int secondsElapsed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to game Manager
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if 60 frames have passed, update secondsElapsed
        if (gameManager.GetComponent<GameManagerBehavior>().frameCounter % 60 == 0)
        {
            secondsElapsed = secondsElapsed + 1;
        }
        //finally, update the timer display according to secondsElapsed
        this.gameObject.GetComponent<TextMeshProUGUI>().text = formatSecondsToTime(secondsElapsed);
    }
    string formatSecondsToTime(int totalSeconds)
    {
        //first, divide by 60 and throw away the non-integer part to get minutes
        int minutesToDisplay = Mathf.FloorToInt(totalSeconds / 60);
        //then, convert that minutes number back into seconds and subtract those seconds from totalSeconds to get the sub-minute seconds
        int subMinuteSeconds = totalSeconds - (minutesToDisplay * 60);
        //finally, format the subMinute seconds for display
        string secondsToDisplay;
        //If less than 10 subMinuteSeconds are remaining, then add a 0 to the display
        if (subMinuteSeconds < 10)
        {
            secondsToDisplay = "0" + subMinuteSeconds;
        }
        //else if there are 10 or more subMinuteSeconds, convert them directly to the display string
        else
        {
            secondsToDisplay = subMinuteSeconds.ToString();
        }
        //concatenate the info
        string timeToDisplay = minutesToDisplay + ":" + secondsToDisplay;
        //finally, return the time to display
        return timeToDisplay;
    }
}
